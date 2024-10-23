using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoBattle.Engine.Graphics;

namespace MonoBattle {
    
    public class MonoBattleGame : Game {

        private GraphicsDeviceManager m_graphics;
        private MonoBattleRenderer m_renderer;

        /// <summary>
        /// Constructor
        /// </summary>
        public MonoBattleGame( ) {
            m_graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        /// <summary>
        /// Resize game.
        /// </summary>
        /// <param name="width" >New game window width</param>
        /// <param name="height" >New game window height</param>
        public void Resize( int width, int height ) {
            m_graphics.PreferredBackBufferWidth  = width;
            m_graphics.PreferredBackBufferHeight = height;
            m_graphics.ApplyChanges( );

            m_renderer.Resize( GraphicsDevice );
        }

        /// <summary>
        /// Initialize game instance.
        /// </summary>
        protected override void Initialize( ) {
            base.Initialize( );
        }

        /// <summary>
        /// Load game content.
        /// </summary>
        protected override void LoadContent( ) {
            m_renderer = new MonoBattleRenderer( GraphicsDevice );
        }

        /// <summary>
        /// Update game instance.
        /// </summary>
        /// <param name="game_time" >Current game time</param>
        protected override void Update( GameTime game_time ) {
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown( Keys.Escape ) )
                Exit( );
        }

        /// <summary>
        /// Draw game instance.
        /// </summary>
        /// <param name="game_time">Current game time</param>
        protected override void Draw( GameTime game_time ) {
            m_renderer.Prepare( );
            m_renderer.Present( );
        }

    }

}
