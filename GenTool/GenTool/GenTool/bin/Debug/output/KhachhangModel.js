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

var Khachhang = function ( KhachhangID, TenKhachhang, Socmnd, SoTienTichluy ) {
    var self = this;

   self.KhachhangID = ko.observable(KhachhangID);
self.TenKhachhang = ko.observable(TenKhachhang);
self.Socmnd = ko.observable(Socmnd);
self.SoTienTichluy = ko.observable(SoTienTichluy);

    // bổ sung
    self.IsEdit = ko.observable(false);
    self.IsVisible = ko.observable(true);
    self.OldValue = ko.observable({});
}

