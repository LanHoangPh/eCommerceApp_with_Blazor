using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IGeneric<TEnity> where TEnity : class
    {
        Task<IEnumerable<TEnity>> GetAllAsync(); // dùng IEnumerable vì nó chỉ hỗ duyệt danh sách foreach thay vì các cấu trúc khác có thể CRUD điều này ko cần thiết(bảo mật) vì chỉ lấy thôi ngoài ra nó còn hỗ trọ trong việc LAZY loading tốt hơn các cấu trúc khác
        Task<TEnity> GetByIdAsync(Guid id);
        Task<int> AddAsync(TEnity entity); // truyền int giúp bán biết đc số bản ghi dc thay đỏi trong việc lấy lại dữ liệu nếu là TEnity còn nếu ko truyền cái gì thì nó chỉ tạo là xong(int tốt hơn nếu nói về hiệu suất nhưng tùy trường hợp bạn cần lấy dữ liệu đac đc tạo thì thêm cx đc
        Task<int> UpdateAsync(TEnity entity);
        Task<int> DeleteAsync(Guid id);
    }
}
