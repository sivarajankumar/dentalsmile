using System;
using System.Windows;
using System.Windows.Controls;

namespace smileUp
{
    public class EllipticalLayoutPanel : Canvas
    {
        #region Properties

        /// <summary>
        /// Gets or sets the X coordinate of the layout ellipse's origin.
        /// This property is backed by the dependency property <see cref="EllipseCentreXProperty"/>.
        /// </summary>
        public double EllipseCentreX
        {
            get { return (double)GetValue(EllipseCentreXProperty); }
            set { SetValue(EllipseCentreXProperty, value); }
        }

        /// <summary>
        /// Dependency property backing store for <see cref="EllipseCentreX"/>.
        /// Changes to this property cause the control to arrange itself, indicated with
        /// the <see cref="FrameworkPropertyMetadataOptions"/>.AffectsArrange value.
        /// </summary>
        public static readonly DependencyProperty EllipseCentreXProperty 
            = DependencyProperty.Register("EllipseCentreX", typeof(double), typeof(EllipticalLayoutPanel), 
                                          new FrameworkPropertyMetadata(400d, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Gets or sets the Y coordinate of the layout ellipse's origin.
        /// This property is backed by the dependency property <see cref="EllipseCentreYProperty"/>.
        /// </summary>
        public double EllipseCentreY
        {
            get { return (double)GetValue(EllipseCentreYProperty); }
            set { SetValue(EllipseCentreYProperty, value); }
        }

        /// <summary>
        /// Dependency property backing store for <see cref="EllipseCentreY"/>.
        /// Changes to this property cause the control to arrange itself, indicated with
        /// the <see cref="FrameworkPropertyMetadataOptions"/>.AffectsArrange value.
        /// </summary>
        public static readonly DependencyProperty EllipseCentreYProperty
            = DependencyProperty.Register("EllipseCentreY", typeof(double), typeof(EllipticalLayoutPanel), 
                                          new FrameworkPropertyMetadata(50d, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Gets or sets the height of the ellipse used to layout child content.
        /// This property is backed by the dependency property <see cref="EllipseHeightProperty"/>.
        /// </summary>
        public double EllipseHeight
        {
            get { return (double)GetValue(EllipseHeightProperty); }
            set { SetValue(EllipseHeightProperty, value); }
        }

        /// <summary>
        /// Dependency property backing store for <see cref="EllipseHeight"/>.
        /// Changes to this property cause the control to arrange itself, indicated with
        /// the <see cref="FrameworkPropertyMetadataOptions"/>.AffectsArrange value.
        /// </summary>
        public static readonly DependencyProperty EllipseHeightProperty
            = DependencyProperty.Register("EllipseHeight", typeof(double), typeof(EllipticalLayoutPanel), 
                                          new FrameworkPropertyMetadata(75d, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Gets or sets the width of the ellipse used to layout child content.
        /// This property is backed by the dependency property <see cref="EllipseWidthProperty"/>.
        /// </summary>
        public double EllipseWidth
        {
            get { return (double)GetValue(EllipseWidthProperty); }
            set { SetValue(EllipseWidthProperty, value); }
        }

        /// <summary>
        /// Dependency property backing store for <see cref="EllipseWidth"/>.
        /// Changes to this property cause the control to arrange itself, indicated with
        /// the <see cref="FrameworkPropertyMetadataOptions"/>.AffectsArrange value.
        /// </summary>
        public static readonly DependencyProperty EllipseWidthProperty
            = DependencyProperty.Register("EllipseWidth", typeof(double), typeof(EllipticalLayoutPanel),
                                          new FrameworkPropertyMetadata(75d, FrameworkPropertyMetadataOptions.AffectsArrange));

        #endregion

        #region Private Overiddes

        /// <summary>
        /// Overrides the arrange method for this control, allowing content to be placed
        /// according to your own layout criteria. In this case we are going to lay
        /// all the child objects out along the edge of an ellipse.
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            //Work out the circumference of our Ellipse as this may have changed.
            double layoutCirfumference 
                = CalculateEllipseCircumferenceRamanujan2(EllipseWidth, EllipseHeight);

            //Calculate the spacing between each item.
            double itemSpacing = layoutCirfumference / Children.Count;            

            //Iterate the children. We could have used a foreach, but
            //as we need to know the index of the child we are working with,
            //this is cleaner.
            for (int i = 0; i < Children.Count; i++)
            {
                //We want the item as a FrameworkElement so that we can
                //get its height and width.
                FrameworkElement child = Children[i] as FrameworkElement;

                if (child == null)
                {
                    continue;
                }

                //Work out how far around the ellipse we are
                double theta = (Math.PI * 2) * ((i * itemSpacing) / layoutCirfumference);
                //Offset so the ellipse starts at the top.
                theta -= Math.PI; /// 2;

                //Now to calculate the point on the edge of the ellipse for the current rotation.
                Point p = GetPointOnEllipse(EllipseWidth, 
                                            EllipseHeight, 
                                            new Point(EllipseCentreX, 
                                                      EllipseCentreY), 
                                            theta);
                
                //Position the element
                SetLeft(child, p.X - child.DesiredSize.Width * 0.5);
                SetTop(child, p.Y - child.DesiredSize.Height * 0.5);

                //Get the child control to arrange itself.
                child.Arrange(new Rect(new Point(p.X, p.Y), child.DesiredSize));
            }
          
            return arrangeSize;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculates the point on an ellipse at the given angle.
        /// </summary>        
        /// <param name="width">The width of the ellipse (length of major axis).</param>
        /// <param name="height">The height of the ellipse (length of minor axis).</param>
        /// <param name="origin">The location of the ellipse's centre.</param>
        /// <param name="thea">The angle (in radians) at which the point of interest lies.</param>
        /// <returns>A <see cref="Point"/> object detailing the coordinates of the point on the ellipse.</returns>
        private static Point GetPointOnEllipse(double width, double height, Point origin, double theta)
        {
            //This code uses the rather expensive Math.Cos() and Math.Sin() methods.
            //If performance is a big issue you may want to consider using a look
            //up table of precomputed angles.
            double x = origin.X + (width * Math.Cos(theta));
            double y = origin.Y + (height * Math.Sin(theta));

            return new Point(x, y);
        }

        /// <summary>
        /// Uses the second Ramanujan approximation to calculate the circumference of an ellipse.
        /// </summary>
        /// <returns>The circumference of an ellipse with a width <param name="a"/> and height <param name="b" />.</returns>
        private static double CalculateEllipseCircumferenceRamanujan2(double a, double b)
        {
            double h = Math.Pow(a - b, 2) / Math.Pow(a + b, 2);

            //pi (a + b) [ 1 + 3 h / (10 + (4 - 3 h)^1/2 ) ]            
            double result = Math.PI * (a + b) * (1 + 3 * h / (10 + Math.Pow((4 - 3 * h), 0.5)));

            return result;
        }

        #endregion
    }
}
