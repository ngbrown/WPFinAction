﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.IO;
using System.Printing;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Xps;

namespace DictionaryPages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private Encoding conversationEncoding = Encoding.UTF8;
        private string defaultServer = "dict.us.dict.org";
        private readonly int defaultPort = 2628;
        private const int bufferSize = 4096;

        public SearchPage()
        {
            InitializeComponent();
        }

        public SearchPage(string word)
            : this()
        {
            searchText.Text = word;
            DefineWord(word);
            Title = "Search - " + word;
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string word = searchText.Text.Trim();
            if (word.Length > 0)
            {
                SearchPage page = new SearchPage(word);
                NavigationService.Navigate(page);
                DefineWord(word);
            }
            Mouse.OverrideCursor = null;
        }

        private void DefineWord(string word)
        {
            string dictionary = "*";
            if (dictionaryToUse.Length > 0)
                dictionary = dictionaryToUse;

            string command = "DEFINE " + dictionary + " " + word;
            string strResult = ExecuteCommand(command);

            FlowDocument doc = new FlowDocument();
            Paragraph para1 = new Paragraph();
            para1.FontSize = 18;
            para1.Inlines.Add(new Bold(new Run(word)));
            doc.Blocks.Add(para1);
            Paragraph paraDate = new Paragraph();
            paraDate.Inlines.Add(new Run("Looked up: " + DateTime.Now.ToString("T")));
            doc.Blocks.Add(paraDate);
            Paragraph para2 = new Paragraph();
            para2.Inlines.Add(new Run(strResult));
            doc.Blocks.Add(para2);
            searchResults.Document = doc;
        }

        private string ExecuteCommand(string command)
        {
            StringBuilder response = new StringBuilder();
            using (TcpClient client = new TcpClient())
            {
                client.Connect(defaultServer, defaultPort);
                using (Stream clientStream = client.GetStream())
                {
                    response.Append(GetResponse(command + "\r\n", clientStream));
                    response.Append(GetResponse("QUIT\r\n", clientStream));
                }
            }
            return response.ToString();
        }

        private string GetResponse(string requestString, Stream clientStream)
        {
            byte[] request = conversationEncoding.GetBytes(requestString);
            clientStream.Write(request, 0, request.Length);
            clientStream.Flush();
            byte[] response = new byte[bufferSize];
            StringBuilder sb = new StringBuilder();
            int currentPosition = 0, bytesRead = 0;
            while ((bytesRead = clientStream.Read(response, 0, bufferSize)) > 0)
            {
                sb.Append(conversationEncoding.GetString(response, 0, bytesRead));
                currentPosition += bytesRead;
                if (bytesRead < bufferSize)
                    break;
            }

            return sb.ToString();
        }

        private static string dictionaryToUse = "*";

        private void OnSelectDictionary(object sender, RoutedEventArgs e)
        {
            SettingsPageFunction pageFunction = new SettingsPageFunction(dictionaryToUse);
            pageFunction.Return += new ReturnEventHandler<string>(OnSettingsPageFunctionReturned);

            NavigationService.Navigate(pageFunction);
        }

        void OnSettingsPageFunctionReturned(object sender, ReturnEventArgs<string> e)
        {
            if (e.Result != null)
            {
                dictionaryToUse = e.Result;
            }
        }

        private void OnPrint(object sender, RoutedEventArgs e)
        {
            FlowDocument docCopy = CopyFlowDocument(searchResults.Document);
            docCopy.PagePadding = new Thickness(96);
            docCopy.ColumnWidth = double.NaN;

            IDocumentPaginatorSource paginatorSource = docCopy as IDocumentPaginatorSource;

            PrintQueue queue = LocalPrintServer.GetDefaultPrintQueue();
            XpsDocumentWriter docWriter = PrintQueue.CreateXpsDocumentWriter(queue);

            docWriter.WritingCompleted += 
                new System.Windows.Documents.Serialization.WritingCompletedEventHandler(docWriter_WritingCompleted);

            docWriter.WriteAsync(paginatorSource.DocumentPaginator);
        }

        void docWriter_WritingCompleted(object sender, System.Windows.Documents.Serialization.WritingCompletedEventArgs e)
        {
            MessageBox.Show("Done Printing!", "Dictionary");
        }

        protected FlowDocument CopyFlowDocument(FlowDocument originalDoc)
        {
            string xmlDoc = XamlWriter.Save(originalDoc);

            StringReader stringReader = new StringReader(xmlDoc);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            FlowDocument newDoc = (FlowDocument)XamlReader.Load(xmlReader);

            return newDoc;
        }

        private void OnPrintFixed(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FixedDocument fixedDocument = new FixedDocument();
                fixedDocument.DocumentPaginator.PageSize = 
                    new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                PageContent firstPage = new PageContent();
                FixedPage fixedPage = new FixedPage();

                Canvas canvas = new Canvas();
                canvas.Width = fixedDocument.DocumentPaginator.PageSize.Width;
                canvas.Height = fixedDocument.DocumentPaginator.PageSize.Height;
                fixedPage.Children.Add(canvas);

                TextBlock tb = new TextBlock();
                tb.Foreground = Brushes.Black;
                tb.FontFamily = new System.Windows.Media.FontFamily("Arial");
                tb.FontSize = 36.0;
                tb.Text = "Hello";
                Canvas.SetTop(tb, 70);
                Canvas.SetLeft(tb, 70);
                canvas.Children.Add(tb);

                Ellipse ell = new Ellipse();
                ell.Width = 400;
                ell.Height = 400;
                ell.StrokeThickness = 3;
                ell.Stroke = new SolidColorBrush(Colors.Black);
                Canvas.SetTop(ell, 200);
                Canvas.SetLeft(ell, 300);
                canvas.Children.Add(ell);

                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Width = (4 * 96);
                border.Height = (6 * 96);
                Canvas.SetLeft(border, 96);
                Canvas.SetTop(border, 3 * 96);
                canvas.Children.Add(border);

                FlowDocument docCopy = CopyFlowDocument(searchResults.Document);
                docCopy.ColumnWidth = double.NaN;
                docCopy.PageWidth = border.Width - 2;
                docCopy.PageHeight = border.Height - 2;
                IDocumentPaginatorSource paginatorSource = docCopy as IDocumentPaginatorSource;
                DocumentPage docPage = paginatorSource.DocumentPaginator.GetPage(0);

                int xPixels = (int)printDialog.PrintTicket.PageResolution.X;
                int yPixels = (int)printDialog.PrintTicket.PageResolution.Y;

                RenderTargetBitmap renderTarget = 
                    new RenderTargetBitmap(
                        xPixels * 4, yPixels * 6, xPixels, yPixels, System.Windows.Media.PixelFormats.Default);
                renderTarget.Render(docPage.Visual);

                Image img = new Image();
                img.Width = docCopy.PageWidth;
                img.Height = docCopy.PageHeight;
                img.Source = renderTarget;
                border.Child = img;

                ((System.Windows.Markup.IAddChild)firstPage).AddChild(fixedPage);
                fixedDocument.Pages.Add(firstPage);

                PrintQueue queue = printDialog.PrintQueue;
                XpsDocumentWriter docWriter = PrintQueue.CreateXpsDocumentWriter(queue);
                docWriter.Write(fixedDocument.DocumentPaginator);
            }
        }
    }
}
