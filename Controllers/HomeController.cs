using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfCR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public FileResult Index(string editor1,string parametr1, string parametr2, string parametr3)
		{
			//// create a unique file name
			string fileName = Guid.NewGuid() + ".pdf";

			int state;

			float TK = 500;//до полу
			float BB1 = (float)((65+1)*10);//ПоБ
			float TT1 = (float)( (48+0.5)*10);//ПоТ
			float TB = 200; //Від Талія-бедро
			float LSbok = (float)((97 -40)*10 );
			float LSzadi = (float)((99 -40)*10 );
			float LSperedi = (float)((96 -40)*10 );

			// Initialize document object
			Document document = new Document();
			// Add page
			Page page = document.Pages.Add();
			page.Rotate = (Rotation)1;

			// Установить поля страницы со всех сторон равными 0
			page.PageInfo.Margin.Left = 10;
			page.PageInfo.Margin.Right = 0;
			page.PageInfo.Margin.Bottom =10;
			page.PageInfo.Margin.Top = 0;
			var top0 = page.PageInfo.Height - 10;
			// Создайте объект Graph с шириной и высотой, равными размерам страницы
			Graph graph = new Graph((float)page.PageInfo.Width, (float)top0);

			float bokShovX = (float)((BB1 / 2 - 1));
			

			//Расчет  вітачки спинки
			//var vitspin = (float)((Math.Abs(BB1 - TT1) * 0.3 / 2));
			var vitspin = (float)((Math.Abs(BB1 - TT1) * 3 / 2));
			//var vitpol = (float)((Math.Abs(BB1 - TT1) * 0.2 / 2));
			var vitpol = (float)((Math.Abs(BB1 - TT1) * 2 / 2));

			float[] k = new float[2] {0,0};
			float[] k2 = new float[2] { BB1, 0 };
			float[] k1 = new float[2] { bokShovX, 0 };


			float[] t = new float[2] { 0, LSzadi };
			float[] t1 = new float[2] { bokShovX, LSbok };
			float[] t2 = new float[2] { BB1, LSperedi };

			/************/

			//Расчет боковой вітачки
			//var bokvit = (float)(((BB1 - TT1) * 0.5 / 2));
			var bokvit = (float)((Math.Abs(BB1 - TT1) / 2));

			float[] t4 = new float[2] ;
			float aglT4 = GetAngle(t1, t2);
			t4 = GetEndPointByTrigonometric(aglT4, t1, bokvit);
			DrawPix(t4, graph, Color.Yellow);
			float[] t3= new float[2];
			float aglT3 = GetAngle(t, t1);
			t3 = GetEndPointByTrigonometric(aglT3, t1, -bokvit);
			DrawPix(t3, graph, Color.Yellow);

			/********************/

			float[] b = new float[2] { 0, LSzadi - 20*10 };
			float[] b1 = new float[2] { bokShovX, LSzadi - 20*10 };
			float[] b2 = new float[2] { BB1, LSzadi - 20*10 };

			/*Расчет вітачек*/
			float bv = (float)(BB1 / 5);
			float[] v1 = new float[2] { b1[0] - bv, b1[1]+4*10 };
			float[] v4 = new float[2] { b1[0] - bv, b1[1]+4*10 };

			/*Точка середині вітачки на талии*/
			/***********************************************************/
			//float[] v0s = new float[2];
			//float lenvitsp = (float)Math.Abs(t[1] - b[1] - 4*10);
			//v0s= GetEndPointByTrigonometric(180, v1, lenvitsp);
			////DrawPix(v0s, graph, Color.Yellow);

			//float[] v0p = new float[2];
			//float lenvitpol = (float)(t[1] - b[1] - 4*10);
			//v0p = GetEndPointByTrigonometric(180, v1, lenvitpol);
			//DrawPix(v0s, graph, Color.Yellow);
			/********************************************************/

			//float[] v2 = new float[2];
			//v2 = GetEndPointByTrigonometric(180, v0s, -vitspin);
			//float[] v3 = new float[2];
			//v3 = GetEndPointByTrigonometric(180, v0s, vitspin);
			//float[] v5 = new float[2];
			//v5 = GetEndPointByTrigonometric(180, v0p, -vitpol);
			//float[] v6 = new float[2];
			//v6 = GetEndPointByTrigonometric(180, v0p, vitpol);

			//Линия низу
			Line line = new Line(new float[] { k[0], k[1], k2[0], k2[1] });
			line.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(line);

			//ТОчка К	
			Circle cicleK = new Circle(0, 0, 1);			
			cicleK.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleK);

			//Точка К2
			Circle cicleK2 = new Circle(k2[0], k2[1], 1);
			cicleK2.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleK2);

			//Линия бока сзади
			Line lineSzadi = new Line(new float[] { 0, 0, 0, LSzadi });
			lineSzadi.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineSzadi);

			//Точка T
			Circle cicleT = new Circle(t[0], t[1], 1);			
			cicleT.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleT);

			//Линия бока спереди
			Line lineSperedi = new Line(new float[] { BB1, 0, BB1, LSperedi });
			lineSperedi.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineSperedi);

			//Точка T2
			Circle cicleT2 = new Circle(t2[0],t2[1], 1);			
			cicleT2.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleT2);

			//Находим положение бокового шва
			//

			//Точка K1
			Circle cicleK1 = new Circle(k1[0], k1[1], 1);			
			cicleK1.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleK1);

			//Точка T1
			//Circle cicleT1 = new Circle(t1[0],t1[1],2);			
			//cicleT1.GraphInfo.FillColor = Color.Gray;
			//graph.Shapes.Add(cicleT1);

			//line K1-B1
			Line lineSbok = new Line(new float[] { k1[0], k1[1], b1[0], b1[1] });
			lineSbok.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineSbok);

            //TT temporary
            Line TempTT = new Line(new float[] { 0, LSzadi, bokShovX, LSbok });
            TempTT.GraphInfo.Color = Color.LightGray;
            graph.Shapes.Add(TempTT);

            Line TempTT1 = new Line(new float[] { bokShovX, LSbok, BB1, LSperedi });
            TempTT1.GraphInfo.Color = Color.LightGray;
            graph.Shapes.Add(TempTT1);

            //Линия бедер
            Line BB2 = new Line(new float[] { 0, LSzadi-20*10, BB1, LSzadi - 20*10 });
			BB2.GraphInfo.Color = Color.LightGray;
			graph.Shapes.Add(BB2);

			//Точка B
			Circle cicleB = new Circle(b[0], b[1], 1);			
			cicleB.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleB);

			//Точка B1
			Circle cicleB1 = new Circle(b1[0], b1[1], 1);			
			cicleB1.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleB1);

			//Точка B2
			Circle cicleB2 = new Circle(b2[0], b2[1], 1);			
			cicleB2.GraphInfo.FillColor = Color.GreenYellow;
			graph.Shapes.Add(cicleB2);

			//Відстань від середини до початку виачки
			//BB2/2

			var TosnVit = (float)BB1/5;

			//Точка TosnVit
			Circle TosnVitS = new Circle(bokShovX- TosnVit, LSzadi - 20*10, 1);
			TosnVitS.GraphInfo.Color = Color.Gray;
			graph.Shapes.Add(TosnVitS);

			//Точка TosnVit
			Circle TosnVitP = new Circle(bokShovX + TosnVit, LSzadi - 20*10, 1);
			TosnVitP.GraphInfo.Color = Color.Gray;
			graph.Shapes.Add(TosnVitP);




            //Т3Б1

            Line lineT3b1 = new Line(new float[] { t3[0], t3[1], b1[0], b1[1] });
            lineT3b1.GraphInfo.Color = Color.Red;
            graph.Shapes.Add(lineT3b1);

            ////Т4Б1

            Line lineT4b1 = new Line(new float[] { t4[0], t4[1], b1[0], b1[1] });
            lineT4b1.GraphInfo.Color = Color.Red;
            graph.Shapes.Add(lineT4b1);


			//Точка пересечения внутренней віточки спина

			float[] vs = new float[2];
			float[] v1tt1 = new float[2] { v1[0], v1[1]+1000 };
			vs = GetIntersectionPointOfTwoLines(t, t1, v1, v1tt1, out state);
			DrawPix(vs, graph, Color.Yellow);

			//Находим V2-V3
			//float aglT3 = GetAngle(t, t1);
			float[] v2 = new float[2];
			v2 = GetEndPointByTrigonometric(aglT3, vs, -15);
			DrawPix(v2, graph, Color.Yellow);

			float[] v3 = new float[2];
			v3 = GetEndPointByTrigonometric(aglT3, vs, 15);
			DrawPix(v3, graph, Color.Yellow);

			////ТV2
			Line lineТV2 = new Line(new float[] { t[0], t[1], v2[0], v2[1] });
			lineТV2.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineТV2);

			////V1V2
			Line lineV1V2 = new Line(new float[] { v2[0], v2[1], v1[0], v1[1] });
			lineV1V2.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineV1V2);

			////V1V3
			Line lineV1V3 = new Line(new float[] { v3[0], v3[1], v1[0], v1[1] });
			lineV1V3.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineV1V3);

			////V3T3
			Line lineV3T3 = new Line(new float[] { v3[0], v3[1], t3[0], t3[1] });
			lineV3T3.GraphInfo.Color = Color.Red;
			graph.Shapes.Add(lineV3T3);








			// Добавить объект Graph в коллекцию абзацев страницы
			page.Paragraphs.Add(graph);

			Stream outputStream = new MemoryStream();

			// Save PDF 
			document.Save(outputStream);

			// return generated PDF file
			return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
		}

		public static void DrawPix(float[] to, Graph graph, Color color)
        {
			Circle cicle = new Circle(to[0], to[1], 5);
			cicle.GraphInfo.FillColor = color;
			graph.Shapes.Add(cicle);
		}

		

		public static float[] GetIntersectionPointOfTwoLines(float []p1_1, float [] p1_2, float[] p2_1, float[] p2_2, out int state)
		{
			state = -2;			
		//	float[] PointF = new float[2];
			float[] result = new float[2];
			//Если знаменатель (n) равен нулю, то прямые параллельны.
			//Если и числитель (m или w) и знаменатель (n) равны нулю, то прямые совпадают.
			//Если нужно найти пересечение отрезков, то нужно лишь проверить, лежат ли ua и ub на промежутке [0,1].
			//Если какая-нибудь из этих двух переменных 0 <= ui <= 1, то соответствующий отрезок содержит точку пересечения.
			//Если обе переменные приняли значения из [0,1], то точка пересечения прямых лежит внутри обоих отрезков.
			float m = ((p2_2[0] - p2_1[0]) * (p1_1[1] - p2_1[1]) - (p2_2[1] - p2_1[1]) * (p1_1[0] - p2_1[0]));
			float w = ((p1_2[0] - p1_1[0]) * (p1_1[1] - p2_1[1]) - (p1_2[1] - p1_1[1]) * (p1_1[0] - p2_1[0])); //Можно обойтись и без этого
			float n = ((p2_2[1] - p2_1[1]) * (p1_2[0] - p1_1[0]) - (p2_2[0] - p2_1[0]) * (p1_2[1] - p1_1[1]));

			float Ua = m / n;
			float Ub = w / n;

			if ((n == 0) && (m != 0))
			{
				state = -1; //Прямые параллельны (не имеют пересечения)
			}
			else if ((m == 0) && (n == 0))
			{
				state = 0; //Прямые совпадают
			}
			else
			{
				
				//Прямые имеют точку пересечения
				result[0] = p1_1[0] + Ua * (p1_2[0] - p1_1[0]);
				result[1] = p1_1[1] + Ua * (p1_2[1] - p1_1[1]);

				// Проверка попадания в интервал
				bool a = result[0] >= p1_1[0]; bool b = result[0] <= p1_1[0]; bool c = result[0] >= p2_1[0]; bool d = result[0] <= p2_1[0];
				bool e = result[1] >= p1_1[1]; bool f = result[1] <= p1_1[1]; bool g = result[1] >= p2_1[1]; bool h = result[1] <= p2_1[1];

				if (((a || b) && (c || d)) && ((e || f) && (g || h)))
				{
					state = 1; //Прямые имеют точку пересечения
				}
			}
			return result;
		}
	
		public static float[] GetEndPointByTrigonometric(double angle, float[] StartPoint, double distance)
        {
            float[] EndPoint = new float[3];

                         // Угол в радианах
            var radian = (angle * Math.PI) / 180;

                         // Рассчитать новую координату r - расстояние между двумя
            EndPoint[0] = (float)(Math.Abs(StartPoint[0] + distance * Math.Cos(radian)));
            EndPoint[1] = (float)(Math.Abs(StartPoint[1] + distance * Math.Sin(radian)));
            EndPoint[2] = 0;

            return EndPoint;
        }
		private static float GetAngle(float[] StartPoint, float[] EndPoint)
		{
			// Получим косинус угла по формуле
			double cos = Math.Round((StartPoint[0] * EndPoint[0] + StartPoint[1] * EndPoint[1]) / (Math.Sqrt(StartPoint[0] * StartPoint[0] + StartPoint[1] * StartPoint[1]) * Math.Sqrt(EndPoint[0] * EndPoint[0] + EndPoint[1] * EndPoint[1])), 9);
			// Вернем arccos полученного значения (в радианах!)
			return (float)Math.Acos(cos);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
