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

var Hanghoa = function ( HanghoaId, TenHanghoa, Giaban, NgayGiaban, NhomHanghoa ) {
    var self = this;

   self.HanghoaId = ko.observable(HanghoaId);
self.TenHanghoa = ko.observable(TenHanghoa);
self.Giaban = ko.observable(Giaban);
self.NgayGiaban = ko.observable(NgayGiaban);
self.NhomHanghoa = ko.observable(NhomHanghoa);

    // bổ sung
    self.IsEdit = ko.observable(false);
    self.IsVisible = ko.observable(true);
    self.OldValue = ko.observable({});
}

