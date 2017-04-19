using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UIKit;
using Newtonsoft.Json;

namespace iOSChartJS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            nfloat width = this.View.Frame.Width;
            nfloat height = this.View.Frame.Height;

            UIWebView chartView = new UIWebView();
            chartView.Frame = new CGRect(
                0,
                (height / 2) - (width / 2),
                width,
                width
            );

            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "ChartJS/");

            List<Item> itemList = new List<Item>()
            {
                new Item() { name = "POTATO",   quantity = 23, color = "rgba(38,255,0,0.4)",    },
                new Item() { name = "TOMATO",   quantity = 13, color = "rgba(232,167,6,0.4)",   },
                new Item() { name = "CABBAGE",  quantity = 5,  color = "rgba(255,0,0,0.4)",     },
                new Item() { name = "CARROT",   quantity = 35, color = "rgba(34,12,232,0.4)",   },
                new Item() { name = "EGGPLANT", quantity = 17, color = "rgba(0,255,175,0.4)",   }
            };

            string jsonItemList = JsonConvert.SerializeObject(itemList);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("    <title></title>");
            html.AppendLine("    <style>");
            html.AppendLine("        .canvasSize {");
            html.AppendLine("            width: 100% !important;");
            html.AppendLine("            height: 100% !important;");
            html.AppendLine("        }");
            html.AppendLine("    </style>");
            html.AppendLine("    <link rel='stylesheet' type='text / css' href='style.css'>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("	 <input type='hidden' id='json' value='" + jsonItemList + "'></div>");
            html.AppendLine("    <div style='height: 100px'>");
            html.AppendLine("        <canvas id='myChart' style='height: 300px; width: 300px; '></canvas>");
            html.AppendLine("    </div>");
            html.AppendLine("    <script src='Chart.min.js'></script>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            chartView.LoadHtmlString(html.ToString(), new NSUrl(contentDirectoryPath, true));
            chartView.ScalesPageToFit = false;
            chartView.BackgroundColor = UIColor.Clear;
            chartView.Layer.ShadowOpacity = 0.0f;
            chartView.Opaque = false;
            chartView.ScrollView.ScrollEnabled = false;
            chartView.ScrollView.Bounces = false;

            this.View.AddSubview(chartView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}