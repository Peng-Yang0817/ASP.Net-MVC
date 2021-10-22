using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnCRUD1013.Models;

namespace LearnCRUD1013.Controllers
{
    public class dbCustmerController : Controller
    {
        // GET: dbCustmer
        dbCustomerEntities db = new dbCustomerEntities();
        public ActionResult Index()
        {
            var customers = db.dbCustomer.OrderBy(m => m.f_Id).ToList();
            return View(customers);
        }
      
        public ActionResult Create() {
            return View();
        }
        //有寫HttpPost的這種類型，代表說用表單的Submit來做傳送
        [HttpPost]
        public ActionResult Create(dbCustomer vCustomer) {
            //這是將網頁上的物件vCustomer加入到dbCustomer，但還未存檔
            db.dbCustomer.Add(vCustomer);
            //將dbCustomer存檔，此時table內才正式加入vCustomer
            db.SaveChanges();
            //再轉跳到Index
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int f_id) {
            //抓資料看table內部哪筆資料的f_ID與網頁上回傳回來的f_id吻合，並將吻合的那筆抓出來
            var customer = db.dbCustomer.Where(m => m.f_Id == f_id).FirstOrDefault();
            //將抓到的那筆資料記錄下跟table說Remove這筆資料
            db.dbCustomer.Remove(customer);
            //將dbCustomer存檔，此時table內才正式將該筆資料刪除
            db.SaveChanges();
            //轉跳到Index
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int f_id) {
            //先把table內f_ID為網頁回傳的f_id的物件抓出來顯示在網頁上
            var customer = db.dbCustomer.Where(m => m.f_Id == f_id).FirstOrDefault();
            //將內容顯示在網頁上，當按下Submit時會觸發HttpPost
            return View(customer);
        }
        [HttpPost]
        //接收網頁上調好的Input Value，並用一個vCustomer的物件來接
        public ActionResult Edit(dbCustomer vCustomer) {
            //把vCustomer的f_Id存放在 f_id
            int f_id = vCustomer.f_Id;
            //比對table這裡面哪裡有f_Id 剛好對應f_id的物件，找到後抓出來定義為Customer
            var customer = db.dbCustomer.Where(m => m.f_Id == f_id).FirstOrDefault();
            //逐一用網頁回傳的物件vCustomer內部的元素來更改Customer的內部元素
            customer.f_Name = vCustomer.f_Name;
            customer.f_Phone = vCustomer.f_Phone;
            customer.f_Address = vCustomer.f_Address;
            //將資料庫存檔
            db.SaveChanges();
            //返回首頁
            return RedirectToAction("Index");
        }

    }
}