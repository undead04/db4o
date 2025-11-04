namespace PROJECTDB.models
{
   public class SinhVien:Nguoi
    {
        public List<DeTaiSinhVien> DeTais = new List<DeTaiSinhVien>();
        public SinhVien(string maSV, int maKhoa, string tenSV, string diaChi, string soDienThoai)
        :base(maSV, maKhoa, tenSV, diaChi, soDienThoai) {
        }
    }
}