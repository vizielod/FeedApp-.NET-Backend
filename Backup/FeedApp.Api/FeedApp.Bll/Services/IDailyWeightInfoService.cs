using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IDailyWeightInfoService
    {
        DailyWeightInfo GetDailyWeightInfo(int DailyWeightInfoId);
        IEnumerable<DailyWeightInfo> GetDailyWeightInfos();
        DailyWeightInfo InsertDailyWeightInfo(DailyWeightInfo newDailyWeightInfo);
        void UpdateDailyWeightInfo(int DailyWeightInfoId, DailyWeightInfo updatedDailyWeightInfo);
        void DeleteDailyWeightInfo(int DailyWeightInfoId);
    }
}
