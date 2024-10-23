using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoBattle.Engine.Graphics {

    internal class MonoBattleRenderer {

        private SpriteBatch m_sprite_batch;
        private MonoBattleRenderCanvas m_canvas;
        private Dictionary<string, Effect> m_effects;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device" >Current graphics device instance</param>
        public MonoBattleRenderer( GraphicsDevice device ) {
            m_sprite_batch = new SpriteBatch( device );
            m_canvas       = new MonoBattleRenderCanvas( device );
            m_effects      = new Dictionary<string, Effect>( );
        }

        /// <summary>
        /// Resize current renderer instance.
        /// </summary>
        /// <param name="device" >Current graphics device instance</param>
        public void Resize( GraphicsDevice device )
            => m_canvas.Resize( device );

        /// <summary>
        /// Prepare render for draw operation.
        /// </summary>
        public void Prepare( )
            => m_canvas.Bind( m_sprite_batch.GraphicsDevice );

        /// <summary>
        /// Begin current sprite batch instance.
        /// </summary>
        /// <param name="effect" >Query shader effect name of empty string for default</param>
        /// <returns>Query effect instance or null for default</returns>
        public Effect Begin( string effect = "" ) {
            var shader_effect = (Effect)null;

            if ( !string.IsNullOrEmpty( effect ) )
                shader_effect = m_effects.GetValueOrDefault( effect );

            m_sprite_batch.Begin( SpriteSortMode.Immediate, samplerState : SamplerState.PointClamp, effect : shader_effect );

            return shader_effect;
        }

        public void Draw( Texture2D texture ) {
            m_sprite_batch.Draw( texture, Vector2.Zero, Color.White );
        }

        /// <summary>
        /// End current sprite batch instance.
        /// </summary>
        public void End( ) 
            => m_sprite_batch.End( );

        /// <summary>
        /// Present previous draw operation to screen.
        /// </summary>
        public void Present( ) {
            m_sprite_batch.GraphicsDevice.SetRenderTarget( null );
            m_sprite_batch.GraphicsDevice.Clear( Color.Black );

            m_sprite_batch.Begin( SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp );
            m_sprite_batch.Draw( m_canvas.Target, m_canvas.Destination, Color.White );
            m_sprite_batch.End( );
        }

    }

}
