    var Hanghoa = function (HanghoaId, Maso, Ten, Giaban, NgayGiaban){
var self=this;
self.HanghoaId = ko.observable(HanghoaId).extend({});
self.Maso = ko.observable(Maso).extend({});
self.Ten = ko.observable(Ten).extend({required: {params: False,message: 'Bạn phải nhập tên sản phẩm'},});
self.Giaban = ko.observable(Giaban).extend({min: {params: 5,message: 'Lon hon 5'},required: {params: False,message: 'sản phẩm phải có giá!'},});
self.NgayGiaban = ko.observable(NgayGiaban).extend({});
self.IsCreate = ko.observable(false);
                            self.IsEdit = ko.observable(false);
                            self.IsVisible = ko.observable(true);
                            self.OldValue = ko.observable({});

                            // Validate.
                            this.Errors = ko.validation.group(this);
                            this.isValid = ko.computed(function () {
                                return self.Errors().length == 0;
                            });
}