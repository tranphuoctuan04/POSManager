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

var Hanghoa = function ( HanghoaId, Ten, Giaban ) {
    var self = this;

   self.HanghoaId = ko.observable(HanghoaId);
self.Ten = ko.observable(Ten);
self.Giaban = ko.observable(Giaban);

    // bổ sung
    self.IsEdit = ko.observable(false);
    self.IsVisible = ko.observable(true);
    self.OldValue = ko.observable({});
}

