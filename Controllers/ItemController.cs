using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Services;

namespace TrainingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public ActionResult GetItem()
        {
            var ItemList = _itemService.GetItems();
            return Ok(ItemList);
        }

        // POST api/<ItemController>
        [HttpPost]
        public IActionResult CreateItem(Item item)
        {
            if (string.IsNullOrWhiteSpace(item.NamaItem))
                return BadRequest("Nama Item tidak boleh kosong atau hanya spasi");

            if (item.NamaItem.Length > 100)
                return BadRequest("Nama Item tidak boleh lebih dari 100 karakter");

            if (item.QTY == null || item.QTY <= 0)
                return BadRequest("QTY harus diisi dan lebih dari 0");

            if (item.TglExpire == null || item.TglExpire <= DateTime.Now)
                return BadRequest("Tanggal kadaluarsa harus diisi dan lebih besar dari hari ini");

            if (string.IsNullOrWhiteSpace(item.Supplier))
                return BadRequest("Supplier tidak boleh kosong atau hanya spasi");

            if (item.Supplier.Length > 100)
                return BadRequest("Nama Supplier tidak boleh lebih dari 100 karakter");

            var cekNamaItem = _itemService.GetItems().FirstOrDefault(x => x.NamaItem == item.NamaItem);
            if (cekNamaItem != null)
                return BadRequest("Nama Item sudah ada");

            var insertItem = _itemService.CreateItem(item);
            if (insertItem)
            {
                return Ok("Insert Item Success");
            }
            return BadRequest("Insert Item Failed");
        }

        // PUT api/<ItemController>
        [HttpPut]
        public IActionResult UpdateItem(Item item)
        {
            if (string.IsNullOrWhiteSpace(item.NamaItem))
                return BadRequest("Nama Item tidak boleh kosong atau hanya spasi");

            if (item.NamaItem.Length > 100)
                return BadRequest("Nama Item tidak boleh lebih dari 100 karakter");

            if (item.QTY == null || item.QTY <= 0)
                return BadRequest("QTY harus diisi dan lebih dari 0");

            if (item.TglExpire == null || item.TglExpire <= DateTime.Now)
                return BadRequest("Tanggal kadaluarsa harus diisi dan lebih besar dari hari ini");

            if (string.IsNullOrWhiteSpace(item.Supplier))
                return BadRequest("Supplier tidak boleh kosong atau hanya spasi");

            if (item.Supplier.Length > 100)
                return BadRequest("Nama Supplier tidak boleh lebih dari 100 karakter");

            var cekNamaItem = _itemService.GetItems().FirstOrDefault(x => x.NamaItem == item.NamaItem && x.Id != item.Id);
            if (cekNamaItem != null)
                return BadRequest("Nama Item sudah ada");

            try
            {
                var updateItem = _itemService.UpdateItem(item);
                if (updateItem)
                {
                    return Ok("Update Item Success");
                }
                return BadRequest("Update Item Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            if (id <= 0)
                return BadRequest("ID tidak valid");

            try
            {
                var deleteItem = _itemService.DeleteItem(id);
                if (deleteItem)
                {
                    return Ok("Delete Item Succes");
                }
                return NotFound("Data tidak ditemukan!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
        }
    }
}
