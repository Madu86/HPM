using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
namespace HPCM_REBUILD
{
    public class GMapMarkerCircle : GMap.NET.WindowsForms.GMapMarker
    {
        #region Properties

        /// <summary>
        /// The pen for the outer circle
        /// </summary>
        public Pen OuterPen { get; set; }

        /// <summary>
        /// The brush for the inner circle
        /// </summary>
        public Brush InnerBrush { get; set; }

        /// <summary>
        /// The brush for the Text
        /// </summary>
        public Brush TextBrush { get; set; }

        /// <summary>
        /// The font for the text
        /// </summary>
        public Font TextFont { get; set; }

        /// <summary>
        /// The text to display inside of the marker 
        /// </summary>
        public String Text { get; set; }


        private int diameter = 10;
 

        /// <summary>
        /// The size of the circle
        /// </summary>
        public int CircleDiameter
        {
            get
            {
                return this.diameter;
            }
            set
            {
                diameter = value;
                this.Size = new System.Drawing.Size(diameter, diameter);
                Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
            }
        }

      


        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of the whole circle</param>
        /// <param name="text">The text in the marker.</param>
        public GMapMarkerCircle(PointLatLng p, Pen outer, Brush inner, int diam, String text)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = new Font("Arial", (int)(diameter / 2));
            this.TextBrush = Brushes.Black;
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The LatLongPoint of the marker.</param>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of the whole circle</param>
        /// <param name="textBrush">The brush for the text.</param>
        public GMapMarkerCircle(PointLatLng p, Pen outer, Brush inner, int diam, String text, Brush textBrush)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = new Font("Arial", (int)(diameter / 2));
            this.TextBrush = textBrush;
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The LatLongPoint of the marker.</param>
        /// <param name="outer">The pen for the outer ring</param>
        /// <param name="inner">The brush for the inner circle.</param>
        /// <param name="diameter">The diameter in pixel of the whole circle</param>
        /// <param name="textBrush">The brush for the text.</param>
        public GMapMarkerCircle(PointLatLng p, Pen outer, Brush inner, int diam, String text, Brush textBrush, Font textFont)
            : base(p)
        {
            OuterPen = outer;
            InnerBrush = inner;
            CircleDiameter = diam;
            this.Text = text;
            this.TextFont = textFont;
            this.TextBrush = textBrush;
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }

        /// <summary>
        /// Render a circle
        /// </summary>
        /// <param name="g"></param>
        public override void OnRender(Graphics g)
        {
            g.FillEllipse(InnerBrush, new Rectangle(LocalPosition.X, LocalPosition.Y, diameter, diameter));
            g.DrawEllipse(OuterPen, new Rectangle(LocalPosition.X, LocalPosition.Y, diameter, diameter));

            if (!String.IsNullOrEmpty(this.Text))
            {
                SizeF sizeOfString = g.MeasureString(this.Text, this.TextFont);
                int x = (LocalPosition.X + diameter / 2) - (int)(sizeOfString.Width / 2);
                int y = (LocalPosition.Y + diameter / 2) - (int)(sizeOfString.Height / 2);
                g.DrawString(this.Text, this.TextFont, this.TextBrush, x, y);
            }
        }





    }
}
