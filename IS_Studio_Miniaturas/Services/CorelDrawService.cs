using Corel.Interop.VGCore;
using System;

namespace IS_Studio_Miniaturas.Services
{
    public interface ICorelDrawService
    {
        void SetPageSize(double width, double height);
        (double Width, double Height) GetPageSize();
        ImportFilter ImportDxfFile(string filePath);
        void CreateMatrix(int columns, int rows, double margin);
    }

    public class CorelDrawService : ICorelDrawService
    {
        private readonly Application _corelApp;

        public CorelDrawService(Application corelApp)
        {
            _corelApp = corelApp;
        }

        public void SetPageSize(double width, double height)
        {
            if (_corelApp.ActiveDocument == null)
                throw new InvalidOperationException("Nenhum documento ativo no CorelDRAW.");

            var doc = _corelApp.ActiveDocument;
            doc.Unit = cdrUnit.cdrMillimeter;
            doc.ReferencePoint = cdrReferencePoint.cdrCenter;

            var page = doc.ActivePage;
            page.SetSize(width, height);
        }

        public (double Width, double Height) GetPageSize()
        {
            if (_corelApp.ActiveDocument == null)
                throw new InvalidOperationException("Nenhum documento ativo no CorelDRAW.");

            var doc = _corelApp.ActiveDocument;
            doc.Unit = cdrUnit.cdrMillimeter;

            var page = doc.ActivePage;
            return (page.SizeWidth, page.SizeHeight);
        }

        public ImportFilter ImportDxfFile(string filePath)
        {
            if (_corelApp.ActiveDocument == null)
                throw new InvalidOperationException("Nenhum documento ativo no CorelDRAW.");

            // Obtém o documento ativo
            Document doc = _corelApp.ActiveDocument;
            doc.Unit = cdrUnit.cdrMillimeter;
            doc.ReferencePoint = cdrReferencePoint.cdrCenter;
            Page pag = doc.ActivePage;
            Layer lyr = pag.CreateLayer("teste");

            // Importa o arquivo DXF
            StructImportOptions op = new StructImportOptions();
            op.ColorConversionOptions.SourceColorProfileList = "sRGB IEC61966-2.1,U.S. Web Coated (SWOP) v2,Dot Gain 20%";
            op.ColorConversionOptions.TargetColorProfileList = "sRGB IEC61966-2.1,U.S. Web Coated (SWOP) v2,Dot Gain 20%";

            ImportFilter flt = _corelApp.ActiveDocument.ActivePage.ActiveLayer.ImportEx(filePath, cdrFilter.cdrAutoSense, op);
            
            return flt;
        }

        public void CreateMatrix(int columns, int rows, double margin)
        {
            if (_corelApp.ActiveDocument == null)
                throw new InvalidOperationException("Nenhum documento ativo no CorelDRAW.");

            var doc = _corelApp.ActiveDocument;
            var page = doc.ActivePage;

            double pageWidth = page.SizeWidth - (2 * margin);
            double pageHeight = page.SizeHeight - (2 * margin);

            double cellWidth = pageWidth / columns;
            double cellHeight = pageHeight / rows;

            //for (int row = 0; row < rows; row++)
            //{
            //    for (int col = 0; col < columns; col++)
            //    {
            //        double posX = margin + (col * cellWidth) + (cellWidth / 2);
            //        double posY = margin + (row * cellHeight) + (cellHeight / 2);

            //        var cell = page.CreateRectangle2(posX - (cellWidth / 2), posY - (cellHeight / 2), cellWidth, cellHeight);
            //        cell.Outline.Width = 0.1;
            //        cell.Outline.Color.CMYKAssign(0, 0, 0, 100);
            //        cell.Fill.UniformColor.CMYKAssign(0, 0, 0, 0);
            //    }
            //}
        }
    }
}