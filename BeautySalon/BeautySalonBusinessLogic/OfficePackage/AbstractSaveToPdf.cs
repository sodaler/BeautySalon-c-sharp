using BeautySalonBusinessLogic.OfficePackage.HelperEnums;
using BeautySalonBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDocManager(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата услуги", "Название", "Кредитные программы", "Процедуры" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var service in info.Services)
            {
                CreateRow(new PdfRowParameters
                {
                    
                    Texts = new List<string> { service.DateAdding.ToShortDateString(),
                                                service.ServiceName,
                                                service.Cosmetics,
                                                service.Procedures},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        public void CreateDocClient(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "5cm", "3cm", "5cm", "4cm"});
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "ФИО", "Дата", "Процедуры", "Услуги" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var order in info.Orders)
            {
                for(int i = 0; i < order.ProcedureServices.Count; i++)
                {
                    if (i == 0)
                    {
                        CreateRow(new PdfRowParameters
                        {

                            Texts = new List<string> { order.OrderName,
                                                order.CreateDate.ToShortDateString(),
                                                order.ProcedureServices[i].Item1.ProcedureName,
                                                string.Join(", ", order.ProcedureServices[i].Item2.Select(cur => cur.ServiceName).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                    else
                    {
                        CreateRow(new PdfRowParameters
                        {

                            Texts = new List<string> { String.Empty, String.Empty,
                                                order.ProcedureServices[i].Item1.ProcedureName,
                                                string.Join(", ", order.ProcedureServices[i].Item2.Select(cur => cur.ServiceName).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                }                             
            }
            SavePdf(info);
        }

        /// <summary>/// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info);
        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info);
    }
}

//string.Join(", ",order.ProcedureServices.Select(cur => cur.Item2).ToList())
/*foreach (var dep in order.ProcedureServices)
                {
                    CreateRow(new PdfRowParameters
                    {

                        Texts = new List<string> { order.OrderName,
                                                order.DateVisit.ToShortDateString(),
                                                dep.Item1.ProcedureName,
                                                string.Join(", ",dep.Item2.Select(cur => cur.ServiceName).ToList())},
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
                }   */