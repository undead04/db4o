# DemoDb4o — Hướng dẫn thiết lập & thiết kế DB hướng đối tượng (db4o)

Tài liệu này mô tả cách thiết lập môi trường sử dụng db4o cho dự án, cách build/run, và nguyên tắc thiết kế cơ sở dữ liệu theo hướng đối tượng dựa trên các lớp trong thư mục `models/`.

## Mục tiêu

- Giúp bạn nhanh chóng thiết lập môi trường để chạy dự án có `db4o`.
- Giải thích cách tổ chức dữ liệu theo hướng đối tượng (Object Database) dựa trên các lớp trong `models/`.

## Thiết lập môi trường — lưu ý về db4objects.Db4o (DLL cần thiết)

Lưu ý quan trọng: gói/phiên bản `Db4objects.Db4o` đã bị ngừng phát hành chính thức. Do vậy cách thiết lập môi trường cho dự án thường yêu cầu bạn có file thư viện (DLL) `Db4objects.Db4o.dll` cục bộ thay vì chỉ cài bằng NuGet từ feed công khai.

Các bước thiết lập môi trường (khi DLL không có trên NuGet công khai):

1. Tải thư viện `Db4objects.Db4o.dll`

   - Tải thư viện tại:  
   👉 https://sourceforge.net/projects/db4o/files/db4o/ (chọn `db4o-8.1-net40.zip`)
   - Giải nén và copy file `Db4objects.Db4o.dll` vào thư mục `lib/` của dự án.
2. Thêm DLL vào dự án
   - Cách A — Thêm bằng Visual Studio: chuột phải vào References -> Add Reference -> Browse -> chọn `lib\Db4objects.Db4o.dll`.
   - Cách B — Thêm trực tiếp vào `csproj` (ví dụ):

```xml
<ItemGroup>
  <Reference Include="Db4objects.Db4o">
     <HintPath>lib\Db4objects.Db4o.dll</HintPath>
  </Reference>
</ItemGroup>
```

    - Cách C — Nếu muốn copy DLL vào thư mục output khi build, thêm:

```xml
<ItemGroup>
  <None Include="lib\Db4objects.Db4o.dll">
     <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

4. Nếu có thể dùng NuGet (tùy môi trường)

   - Một số tổ chức lưu fork/private feed của db4o; nếu bạn có feed nội bộ, có thể dùng `dotnet add package Db4objects.Db4o --source <your-feed>`.

5. Build & run (PowerShell)

```powershell
cd "d:\Hệ thống thông tin\Lab 5\ProjectDB"
dotnet restore
dotnet build
dotnet run --project .\DemoDb4o.csproj
```

Sau khi dự án được tham chiếu đúng DLL, ứng dụng sẽ mở hoặc tạo file `project.db4o` ở gốc nếu code trong `Program.cs` thực hiện thao tác mở/khởi tạo DB.

> Ghi chú an toàn: Vì db4o không còn được phát hành chính thức, hãy kiểm tra nguồn DLL (không lấy từ nguồn không rõ ràng) và cân nhắc kiểm tra binary bằng công cụ an ninh trước khi dùng trong môi trường sản xuất.

## Cấu trúc dự án liên quan tới DB

- `models/` — chứa các lớp đối tượng (entity) mà db4o sẽ lưu. Ví dụ: `SinhVien.cs`, `GiaoVien.cs`, `DeTai.cs`, `Khoa.cs`, `HoiDong.cs`, `Diem.cs`, `TaiLieu.cs`, `NamHoc.cs`, `Nguoi.cs`.
- `project.db4o` — file cơ sở dữ liệu db4o (binary) nằm ở gốc.

## Thiết kế DB hướng đối tượng (OODB) — nguyên tắc và cách áp dụng trong `models/`

1. Nguyên tắc chung

- Lưu trực tiếp các thể hiện (instances) của lớp (objects) — không cần mapping sang bảng như RDBMS.
- Mỗi đối tượng giữ trạng thái và quan hệ tới đối tượng khác thông qua tham chiếu (field chứa đối tượng khác hoặc collection).

2. Danh sách model & vai trò (tham khảo các file trong `models/`)

- `Nguoi` — lớp cơ sở chứa thuộc tính chung (tên, mã, v.v.). `SinhVien` và `GiaoVien` có thể kế thừa từ `Nguoi`.
- `SinhVien` — thông tin sinh viên; tham chiếu tới `Khoa`, danh sách `DeTai` và `Diem`.
- `GiaoVien` — thông tin giảng viên; có thể là thành viên `HoiDong` hoặc hướng dẫn `DeTai`.
- `DeTai` — đề tài/luận văn; tham chiếu tới `SinhVien`, `GiaoVien` (hướng dẫn), v.v.
- `HoiDong` — hội đồng bảo vệ; chứa collection các `GiaoVien` và tham chiếu tới `DeTai`.
- `Diem` — điểm, có thể liên kết `SinhVien` và `DeTai`.
- `TaiLieu` — tài liệu tham khảo, có thể tham chiếu tới `DeTai` hoặc `GiaoVien`.
- `Khoa`, `NamHoc` — các đối tượng chuyên ngành/niên khoá hỗ trợ phân tầng dữ liệu.

3. Thiết kế quan hệ

- Quan hệ 1-n hoặc n-n được thể hiện bằng tham chiếu trong class (ví dụ `List<DeTai> DeTaiCuaSinhVien;`).
- Không cần bảng trung gian như RDBMS; db4o lưu object graph. Hãy cân nhắc về vòng tham chiếu (circular references) khi serializing.

4. Quy tắc đặt khóa / nhận dạng

- db4o quản lý identity nội bộ cho các đối tượng đã được lưu. Nếu cần khóa rõ ràng, bổ sung trường mã (ID) trong lớp (ví dụ `public string MaSinhVien { get; set; }`) và xử lý unique trong logic ứng dụng.

6. Truy vấn

- db4o hỗ trợ nhiều cách truy vấn: Query-By-Example (QBE), S.O.D.A, Native Queries hoặc LINQ (tuỳ phiên bản). Chọn kiểu truy vấn phù hợp cho độ phức tạp của yêu cầu.

7. Giao dịch & đồng bộ

- Sử dụng transaction/commit khi thực hiện nhiều thao tác liên quan: save (store) nhiều đối tượng xong gọi commit/close.

8. Versioning / Migration

- Khi thay đổi model (thêm/xóa field), db4o có khả năng đọc các object cũ — nhưng tốt nhất nên kiểm tra và thực hiện migration thủ công khi thay đổi quan trọng:
  - Thêm field: đảm bảo code xử lý giá trị null/giá trị mặc định khi đọc dữ liệu cũ.
  - Thay đổi kiểu field hoặc cấu trúc: viết script migration đọc object cũ, chuyển đổi và lưu lại.

## Ví dụ ngắn — lưu và truy vấn (C# minh họa)

Đoạn mã dưới đây là minh hoạ ý tưởng chung (cú pháp tham khảo; kiểm tra API thực tế của package bạn dùng):

```csharp
using Db4objects.Db4o;
using Db4objects.Db4o.Config;

var config = Db4oEmbedded.NewConfiguration();
// nếu cần: config.Common.AddIndex(typeof(SinhVien), "MaSinhVien");
using (IObjectContainer db = Db4oEmbedded.OpenFile(config, "project.db4o"))
{
    // Tạo và lưu đối tượng
    var sv = new SinhVien { MaSinhVien = "SV001", Ten = "Nguyen Van A" };
    db.Store(sv);

    // Commit/close sẽ flush thay đổi
}

// Truy vấn (ví dụ Query-By-Example)
using (IObjectContainer db = Db4oEmbedded.OpenFile("project.db4o"))
{
    var example = new SinhVien { MaSinhVien = "SV001" };
    var result = db.QueryByExample(example);
    foreach (var r in result)
    {
        var s = (SinhVien)r;
        Console.WriteLine(s.Ten);
    }
}
```

## Tài liệu tham khảo

- Xem tài liệu của package `Db4objects.Db4o` (nếu dùng NuGet) để biết chi tiết API và cấu hình.

---

Tệp README này cung cấp hướng dẫn cơ bản và nguyên tắc thiết kế. Nếu bạn muốn, tôi có thể:

- Bổ sung đoạn mã thực tế tương thích chính xác với phiên bản `Db4objects.Db4o` đang dùng trong dự án.
- Tạo script migration mẫu (C#) để cập nhật object khi thay đổi model.

File đã tạo: `README.md` (gốc dự án)
