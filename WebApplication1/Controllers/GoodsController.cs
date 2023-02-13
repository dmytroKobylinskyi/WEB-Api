using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab4.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Linq;

namespace lab4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class GoodsController : Controller
    {
        public List<Good> goods = new List<Good>();

        public GoodsController()
        {
            using (PlumbingShopContext db = new PlumbingShopContext()) {
                foreach (Good g in db.Goods) {
                    goods.Add(g);
                }
            }
        }
        [HttpGet]
        public IEnumerable<Good> Get() => goods;

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var good = goods.SingleOrDefault(g => g.idGoods == id);

            if (good == null) { 
                return NotFound();
            }
            
            return Ok(good);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var good = goods.SingleOrDefault(g => g.idGoods == id);
            if (good != null)
            {
                goods.Remove(good);
                using (PlumbingShopContext db = new PlumbingShopContext())
                {
                    good = db.Goods.SingleOrDefault(g => g.idGoods == id);
                    db.Goods.Remove(good);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        private int NextGoodId =>
            (int)(goods.Count == 0 ? 1 : goods.Max(x => x.idGoods) + 1);

        [HttpGet("GetNextGoodId")]
        public int GetNextGoodId() { 
            return NextGoodId;
        }

        [HttpPost]
        public IActionResult Post(Good good) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            good.idGoods = NextGoodId;
            goods.Add(good);
            using (PlumbingShopContext db = new PlumbingShopContext())
            {
                db.Goods.Add(good);
                db.SaveChanges();
            }
            return CreatedAtAction(nameof(Get), new { id = good.idGoods }, good);
        }

        [HttpPost("AddGood")]
        public IActionResult PostBody([FromBody]Good good) =>
                Post(good);

        [HttpPut]
        public IActionResult Put(Good good) {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var storedGood = goods.SingleOrDefault(g => g.idGoods == good.idGoods);
            if(storedGood == null) 
                return NotFound();
            storedGood.name_goods = good.name_goods;
            storedGood.idGroup = good.idGroup;
            storedGood.price = good.price;
            using (PlumbingShopContext db = new PlumbingShopContext())
            {
                var goodDel = db.Goods.SingleOrDefault(g => g.idGoods == good.idGoods);
                db.Goods.Remove(goodDel);
                db.Goods.Add(storedGood);
                db.SaveChanges();
            }
            return Ok(storedGood);
        }

        [HttpPut("UpdateGood")]
        public IActionResult PutBody([FromBody] Good good) =>
                Put(good);
        // GET: GoodsController
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}
