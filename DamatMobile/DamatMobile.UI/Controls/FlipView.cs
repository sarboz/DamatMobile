using System.Threading.Tasks;
using Xamarin.Forms;

namespace DamatMobile.Ui.Controls
{
    /// <summary>
    /// Flip View Animation Control built with pure Xamarin.Forms 
    /// </summary>
    public class FlipView : ContentView
    {
        private readonly RelativeLayout _contentHolder;
        
        public FlipView()
        {
            _contentHolder = new RelativeLayout();
            Content = _contentHolder;
        }

        public static readonly BindableProperty FrontViewProperty =
            BindableProperty.Create(
                nameof(FrontView),
                typeof(View),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                FrontViewPropertyChanged);

        private static void FrontViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                ((FlipView)bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipView)bindable).FrontView,
                        Constraint.Constant(0),
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.RelativeToParent((parent) => parent.Height)
                    );
            }
        }

        /// <summary>
        /// Gets or Sets the front view
        /// </summary>
        public View FrontView
        {
            get => (View)this.GetValue(FrontViewProperty);
            set => this.SetValue(FrontViewProperty, value);
        }



        public static readonly BindableProperty BackViewProperty =
            BindableProperty.Create(
                nameof(BackView),
                typeof(View),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                BackViewPropertyChanged);

        private static void BackViewPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //Set BackView Rotation before rotating
            if (newvalue != null)
            {
                ((FlipView) bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipView) bindable).BackView,
                        Constraint.Constant(0),
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.RelativeToParent((parent) => parent.Height)
                    );

                ((FlipView)bindable).BackView.IsVisible = false;
            }
        }

        /// <summary>
        /// Gets or Sets the back view
        /// </summary>
        public View BackView
        {
            get => (View)this.GetValue(BackViewProperty);
            set => this.SetValue(BackViewProperty, value);
        }



        public static readonly BindableProperty IsFlippedProperty =
            BindableProperty.Create(
                nameof(IsFlipped),
                typeof(bool),
                typeof(FlipView),
                false,
                BindingMode.Default,
                null,
                IsFlippedPropertyChanged);

        /// <summary>
        /// Gets or Sets whether the view is already flipped
        /// ex : 
        /// </summary>
        public bool IsFlipped
        {
            get => (bool)this.GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }

        private static void IsFlippedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                ((FlipView)bindable).FlipFromFrontToBack();
            }
            else
            {
                ((FlipView)bindable).FlipFromBackToFront();
            }
        }
        
        /// <summary>
        /// Performs the flip
        /// </summary>
        private async void FlipFromFrontToBack()
        {
            await FrontToBackRotate();

            // Change the visible content
            this.FrontView.IsVisible = false;
            this.BackView.IsVisible = true;

            await BackToFrontRotate();
        }

        /// <summary>
        /// Performs the flip
        /// </summary>
        private async void FlipFromBackToFront()
        {
            await FrontToBackRotate();

            // Change the visible content
            this.BackView.IsVisible = false;
            this.FrontView.IsVisible = true;

            await BackToFrontRotate();
        }
        
        #region Animation Stuff

        private async Task<bool> FrontToBackRotate()
        {
            ViewExtensions.CancelAnimations(this);

            this.RotationY = 360;

            await this.RotateYTo(270, 500, Easing.Linear);

            return true;
        }

        private async Task<bool> BackToFrontRotate()
        {
            this.CancelAnimations();

            this.RotationY = 90;

            await this.RotateYTo(0, 500, Easing.Linear);

            return true;
        }

        #endregion
    }
}