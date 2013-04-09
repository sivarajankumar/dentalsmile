using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace smileUp
{
    public class ImageToggleButton : ToggleButton
    {
        Image _image = null;
        TextBlock _textBlock = null;
        StackPanel panel = new StackPanel();

        public ImageToggleButton(BitmapImage bmp, String text)
        {
            panel.Orientation = Orientation.Horizontal;

            panel.Margin = new System.Windows.Thickness(0.1);


            _textBlock = new TextBlock();
            _textBlock.Text = text;
            panel.Children.Add(_textBlock);

            _image = new Image();
            _image.Source = bmp;
            _image.Margin = new System.Windows.Thickness(0.1, 0, 0.1, 0);
            panel.Children.Add(_image);

            this.Content = panel;
        }

        // Properties
        public Image Image
        {
            get { return _image; }
            set {
                panel.Children.Remove(this._image);    
                this._image = value;
                panel.Children.Add(this._image);
            }
        }

        public TextBlock Text
        {
            get { return _textBlock; }
            set {
                panel.Children.Remove(this._textBlock);
                this._textBlock = value;
                panel.Children.Add(this._textBlock);
            }
        }
    }
}
