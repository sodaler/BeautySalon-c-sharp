﻿using BeautySalonBusinessLogic.OfficePackage.HelperEnums;
using BeautySalonBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReportManager(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var lpd in info.CosmeticProcedure)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = lpd.CosmeticName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var dep in lpd.Procedures)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = dep.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = dep.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
               
            }
            SaveExcel(info);
        }
        public void CreateReportClient(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var cc in info.OrderService)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = cc.OrderName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var service in cc.Services)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "A",
                        RowIndex = rowIndex,
                        Text = service,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }

            }
            SaveExcel(info);
        }
        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(ExcelInfo info);
        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="cellParameters"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters
        excelParams);
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParameters"></param>
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
