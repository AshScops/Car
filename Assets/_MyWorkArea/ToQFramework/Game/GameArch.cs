using UnityEngine;

namespace QFramework.Car
{
    public class GameArch : Architecture<GameArch>
    {
        protected override void Init()
        {
            ResKit.Init();
            UIKit.Config.Root.SetResolution(1920, 1080, 0.5f);

            //Models
            this.RegisterModel(new GameModel());
            this.RegisterModel(new PlayerModel());
            this.RegisterModel(new EnemyModel());
            this.RegisterModel(new WeaponModel());
            this.RegisterModel(new ItemModel());

            //Systems
            this.RegisterSystem(new GameSystem()); 

            //TODO:³É¾Í

            //Utilities


        }




    }

}
