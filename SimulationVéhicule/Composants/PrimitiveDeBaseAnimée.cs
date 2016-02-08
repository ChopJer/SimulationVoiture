using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SimulationVéhicule
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class PrimitiveDeBaseAnimée : PrimitiveDeBase
    {
        protected float AngleLacet { get; set; }
        protected float AngleRoulis { get; set; }
        protected float AngleTangage { get; set; }
        protected InputManager GestionInput { get; }
        protected bool MondeÀRecalculer { get; set; }

        public PrimitiveDeBaseAnimée(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ)
            : base(game, homothétieInitiale, rotationInitiale, positionInitiale)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void CalculerMatriceMonde()
        {

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
