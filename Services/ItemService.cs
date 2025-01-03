using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;

namespace TrainingProjectAPI.Services
{
    public class ItemService
    {
        private readonly ApplicationContext _context;

        public ItemService(ApplicationContext context)
        {
            _context = context;
        }

        // Mendapatkan semua item
        public List<Item> GetItems()
        {
            return _context.Items.ToList();
        }

        // Membuat item baru
        public bool CreateItem(Item item)
        {
            try
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Memperbarui item
        public bool UpdateItem(Item item)
        {
            try
            {
                var itemOld = _context.Items.FirstOrDefault(x => x.Id == item.Id);
                if (itemOld != null)
                {
                    itemOld.NamaItem = item.NamaItem;
                    itemOld.QTY = item.QTY;
                    itemOld.TglExpire = item.TglExpire;
                    itemOld.Supplier = item.Supplier;
                    itemOld.AlamatSupplier = item.AlamatSupplier;

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Menghapus item
        public bool DeleteItem(int id)
        {
            try
            {
                var itemData = _context.Items.FirstOrDefault(Del => Del.Id == id);
                if (itemData != null)
                {
                    _context.Items.Remove(itemData);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
