using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Services
{
    public class DailyWeightInfoService :IDailyWeightInfoService
    {
        private readonly ApplicationDbContext _context;


        public DailyWeightInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        //DELETE
        public void DeleteDailyWeightInfo(int DailyWeightInfoId)
        {
            _context.DailyWeightInfos.Remove(new DailyWeightInfo { ID = DailyWeightInfoId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("DailyWeightInfo not found");
            }

        }

        //GET entity
        public DailyWeightInfo GetDailyWeightInfo(int DailyWeightInfoId)
        {
            return _context.DailyWeightInfos.SingleOrDefault(ui => ui.ID == DailyWeightInfoId) ?? throw new EntityNotFoundException("DailyWeightInfo not found!");
        }

        public IEnumerable<DailyWeightInfo> GetDailyWeightInfos()
        {
            var DailyWeightInfos = _context.DailyWeightInfos.ToList();

            return DailyWeightInfos;
        }

        public DailyWeightInfo InsertDailyWeightInfo(DailyWeightInfo newDailyWeightInfo)
        {
            _context.DailyWeightInfos.Add(newDailyWeightInfo);

            _context.SaveChanges();

            return newDailyWeightInfo;
        }

        public void UpdateDailyWeightInfo(int DailyWeightInfoId, DailyWeightInfo updatedDailyWeightInfo)
        {
            updatedDailyWeightInfo.ID = DailyWeightInfoId;
            var entry = _context.Attach(updatedDailyWeightInfo);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("DailyWeightInfo not found!");
            }
        }

    }
}
