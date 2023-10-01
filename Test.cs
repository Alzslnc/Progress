using Autodesk.AutoCAD.Runtime;
using BaseFunction;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Progress
{
    public class Test
    {
        [CommandMethod("test002")]
        public void test002()
        {
            //запускаем класс прогресса
            using (ProgressDialog dialog = new ProgressDialog())
            {
                //устанаваливаем частоту обновления
                dialog.Delay = 200;
                //основаная надпись
                dialog.MainMessage = "Строим круги";
                //надрись при нажатии на клавишу остановки
                dialog.MainCancelMessage = "Останавливаем постройку кругов";
                //используем второй бар
                dialog.UseSubBar = true ;
                //число элементов в основном баре
                dialog.SetMainBarCount = 2;
                int i = 0;
                //запускаем бар
                dialog.Start();
                while (i < 30)
                {
                    //число в счетчике дополнительного бара
                    dialog.SetSubBarCount = 30;
                    i++;
                    using (Circle c = new Circle(new Point3d(0, i, 0), Vector3d.ZAxis, 0.5))
                    {
                        c.ColorIndex = 1;
                        F.AddEntityInCurrentBTR(c);
                    }
                    System.Threading.Thread.Sleep(300);
                    //текст дополнительного счетчика
                    dialog.SubMessage = "Построено красных кругов - " + i + " из 30";
                    //делаем шаг дополнительного счетчика
                    dialog.StepSubBar();
                    if (dialog.IsStopNeed) return;
                }
                //делаем шаг в основном счетчике
                dialog.StepMainBar();
                i = 0;
                while (i < 30)
                {
                    dialog.SetSubBarCount = 30;
                    i++;
                    using (Circle c = new Circle(new Point3d(10, i, 0), Vector3d.ZAxis, 0.5))
                    {
                        c.ColorIndex = 3;
                        F.AddEntityInCurrentBTR(c);
                    }
                    System.Threading.Thread.Sleep(300);
                    dialog.SubMessage = "Построено зеленых кругов - " + i + " из 30";
                    dialog.StepSubBar();
                    if (dialog.IsStopNeed) return;
                }
            }
        }

    }
}
