var AjaxObj = function () {
    var self = this;

    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            async: false,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (self.SuccessHandleFunction != null) {
                    self.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert('Lỗi không thể kết nối đến server.');
            }
        });
    };
};

var Nhanvien = function ( MaNhanvien, TenNhanvien, SoCMND, Ngayvaolam, Luongthang ) {
    var self = this;

   self.MaNhanvien = ko.observable(MaNhanvien);
self.TenNhanvien = ko.observable(TenNhanvien);
self.SoCMND = ko.observable(SoCMND);
self.Ngayvaolam = ko.observable(Ngayvaolam);
self.Luongthang = ko.observable(Luongthang);

    // bổ sung
    self.IsEdit = ko.observable(false);
    self.IsVisible = ko.observable(true);
    self.OldValue = ko.observable({});
}

