using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoBattle.Engine.Graphics {

    internal class MonoBattleRenderCanvas {

        private readonly RenderTarget2D m_target;
        private Rectangle m_destination;

        public RenderTarget2D Target => m_target;
        public Rectangle Destination => m_destination;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device" >Current graphics device instance</param>
        public MonoBattleRenderCanvas( GraphicsDevice device ) {
            var bounds = device.PresentationParameters.Bounds;

            m_target      = new RenderTarget2D( device, bounds.Width, bounds.Height );
            m_destination = new Rectangle( 0, 0, bounds.Width, bounds.Height );

            Resize( device );
        }

        /// <summary>
        /// Resize the canvas drawable rectangle.
        /// </summary>
        /// <param name="device" >Current graphics device instance</param>
        public void Resize( GraphicsDevice device ) {
            var screen_bounds = device.PresentationParameters.Bounds;

            var scale_x = (float)screen_bounds.Width  / m_target.Width;
            var scale_y = (float)screen_bounds.Height / m_target.Height;
            var scale   = MathF.Min( scale_y, scale_x );

            m_destination.Width  = (int)( m_target.Width  * scale );
            m_destination.Height = (int)( m_target.Height * scale );
            m_destination.X      = ( screen_bounds.Width  - m_destination.Width  ) / 2;
            m_destination.Y      = ( screen_bounds.Height - m_destination.Height ) / 2;
        }

        /// <summary>
        /// Bind current canvas to graphics device for rendering.
        /// </summary>
        /// <param name="device" >Current graphics device instance</param>
        public void Bind( GraphicsDevice device ) {
            device.SetRenderTarget( m_target );
            device.Clear( Color.Red );
        }

    }

}
