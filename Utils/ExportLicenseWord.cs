using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using LIcensesPO.Models;
using Word = Microsoft.Office.Interop.Word;

namespace LIcensesPO.Utils;

public static class ExportLicenseWord
{
    public static string Export(License license)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "License.docx");
        
        // Создаем экземпляр приложения Word
        Word.Application wordApp = new Word.Application();

        // Создаем новый документ Word
        Word.Document doc = wordApp.Documents.Open(filePath);

        // Добавляем закладки и вставляем данные
        var marks = createMarks(license);
        foreach (var mark in marks)
        {
            AddDataToBookmark(doc, mark.BookMarkName, mark.BookMarkValue);
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), $"License_{doc.GetHashCode()}.docx");
        // Сохраняем документ
        doc.SaveAs(path);

        // Закрываем Word
        wordApp.Quit();
        
        return path;
    }

    private static List<BookMark> createMarks(License license)
    {
        return new List<BookMark>
        {
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "START",
                BookMarkValue = license.StartDate.ToString(CultureInfo.CurrentCulture)
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "END",
                BookMarkValue = license.EndDate.ToString(CultureInfo.CurrentCulture)
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "PRICE",
                BookMarkValue = license.Price.ToString()
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "NAME",
                BookMarkValue = license.Name
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "COMPUTER",
                BookMarkValue = license.Computer.Name
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "PROGRAM",
                BookMarkValue = license.Prog.Name
            },
            new BookMark()
            {
                BookMarkType = BookMarkType.Text,
                BookMarkName = "LICENSOR",
                BookMarkValue = license.Licensor.Name
            }
        };
    }
    static void AddDataToBookmark(Word.Document doc, string bookmarkName, string data)
    {
        // Проверяем наличие закладки в документе
        if (doc.Bookmarks.Exists(bookmarkName))
        {
            // Получаем объект закладки
            Word.Bookmark bookmark = doc.Bookmarks[bookmarkName];

            // Переходим к закладке
            bookmark.Range.Select();

            // Вставляем данные
            doc.Application.Selection.TypeText(data);
        }
    }
}