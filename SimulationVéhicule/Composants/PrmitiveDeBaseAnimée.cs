using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace SimulationVéhicule
{
    public abstract class PrimitiveDeBaseAnimée : PrimitiveDeBase
    {
        float Homothétie { get; set; }
        Vector3 Position { get; set; }
        float IntervalleMAJ { get; set; }
        protected InputManager GestionInput { get; private set; }
        float TempsÉcouléDepuisMAJ { get; set; }
        float IncrémentAngleRotation { get; set; }
        bool Lacet { get; set; }
        bool Tangage { get; set; }
        bool Roulis { get; set; }
        protected bool MondeÀRecalculer { get; set; }

        float angleLacet;
        protected float AngleLacet
        {
            get
            {
                if (Lacet)
                {
                    angleLacet += IncrémentAngleRotation;
                    MathHelper.WrapAngle(angleLacet);
                }
                return angleLacet;
            }
            set { angleLacet = value; }
        }

        float angleTangage;
        protected float AngleTangage
        {
            get
            {
                if (Tangage)
                {
                    angleTangage += IncrémentAngleRotation;
                    MathHelper.WrapAngle(angleTangage);
                }
                return angleTangage;
            }
            set { angleTangage = value; }
        }

        float angleRoulis;
        protected float AngleRoulis
        {
            get
            {
                if (Roulis)
                {
                    angleRoulis += IncrémentAngleRotation;
                    MathHelper.WrapAngle(angleRoulis);
                }
                return angleRoulis;
            }
            set { angleRoulis = value; }
        }


        protected PrimitiveDeBaseAnimée(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ)
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale)
        {
            IntervalleMAJ = intervalleMAJ;
        }

        public override void Initialize()
        {
            Homothétie = HomothétieInitiale;
            InitialiserRotations();
            Position = PositionInitiale;
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            IncrémentAngleRotation = MathHelper.Pi * IntervalleMAJ / 2;
            TempsÉcouléDepuisMAJ = 0;
            base.Initialize();
        }

        protected override void CalculerMatriceMonde()
        {
            Monde = Matrix.Identity *
                    Matrix.CreateScale(Homothétie) *
                    Matrix.CreateFromYawPitchRoll(AngleLacet, AngleTangage, AngleRoulis) *
                    Matrix.CreateTranslation(Position);
        }

        public override void Update(GameTime gameTime)
        {
            GérerClavier();
            float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TempsÉcouléDepuisMAJ += TempsÉcoulé;
            if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
            {
                EffectuerMiseÀJour();
                TempsÉcouléDepuisMAJ -= IntervalleMAJ;
            }
            base.Update(gameTime);
        }

        protected virtual void EffectuerMiseÀJour()
        {
            if (MondeÀRecalculer)
            {
                CalculerMatriceMonde();
                MondeÀRecalculer = false;
            }
        }

        private void InitialiserRotations()
        {
            AngleLacet = RotationInitiale.Y;
            AngleTangage = RotationInitiale.X;
            AngleRoulis = RotationInitiale.Z;
        }

        protected virtual void GérerClavier()
        {
            if (GestionInput.EstEnfoncée(Keys.LeftControl) || GestionInput.EstEnfoncée(Keys.RightControl))
            {
                if (GestionInput.EstNouvelleTouche(Keys.Space))
                {
                    InitialiserRotations();
                    MondeÀRecalculer = true;
                }
                if (GestionInput.EstNouvelleTouche(Keys.D1) || GestionInput.EstNouvelleTouche(Keys.NumPad1))
                {
                    Lacet = !Lacet;
                }
                if (GestionInput.EstNouvelleTouche(Keys.D2) || GestionInput.EstNouvelleTouche(Keys.NumPad2))
                {
                    Tangage = !Tangage;
                }
                if (GestionInput.EstNouvelleTouche(Keys.D3) || GestionInput.EstNouvelleTouche(Keys.NumPad3))
                {
                    Roulis = !Roulis;
                }
            }
            MondeÀRecalculer = MondeÀRecalculer || Lacet || Tangage || Roulis;
        }
    }
}