using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class AchieveModel : AbstractModel
    {
        public List<AchieveInfo> AchieveInfoList;

        protected override void OnInit()
        {
            JsonUtil.InitJsonData("Achieve", ref AchieveInfoList);
        }



    }

}
