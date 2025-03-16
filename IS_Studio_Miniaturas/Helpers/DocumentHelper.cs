namespace IS_Studio_Miniaturas.Helpers
{
    public static class DocumentHelper
    {
        /// <summary>
        /// Configura as propriedades padrão de um documento do CorelDRAW.
        /// </summary>
        /// <param name="doc">O documento a ser configurado.</param>
        public static void ConfigureDocument(Corel.Interop.VGCore.Document doc)
        {
            doc.Unit = Corel.Interop.VGCore.cdrUnit.cdrMillimeter;
            doc.ReferencePoint = Corel.Interop.VGCore.cdrReferencePoint.cdrCenter;
        }
    }
}