using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecordBook.DAL;
using RecordBook.Models;

namespace RecordBook.Controllers
{
    public class RecordsController : Controller
    {
         IRecordRepository _recordRepo; // = new EFRepository();
                //new SqlCeRepository(); // SqlRepository(); // new MemoryRecordRepository();

        public RecordsController(IRecordRepository repository)
        {
            this._recordRepo = repository;
        }

        public ActionResult Index()
        {
            return View(_recordRepo.GetRecords());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Record record)
        {
            if (ModelState.IsValid)
            {
                record.Date = DateTime.Now;
                _recordRepo.CreateRecord(record);
                return RedirectToAction("Index");
            }
            return View(record);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Record record = _recordRepo.GetRecord(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult Edit(Record record)
        {
            if (ModelState.IsValid)
            {
                _recordRepo.Update(record);
                return RedirectToAction("Index");
            }

            return View(record);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var record = _recordRepo.GetRecord(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult Delete(Record rec)
        {
            _recordRepo.Delete(rec);
            return RedirectToAction("Index");
        }
    }

}