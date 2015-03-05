var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            async: false,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (AjaxInstance.SuccessHandleFunction != null) {
                    AjaxInstance.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert('Lỗi không thể kết nối đến server.' );
            }
        });
    };
};
var AjaxInstance = new AjaxObj();

var HanghoaViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");

    // Danh sách nhóm hàng hóa để đổ ra combobox
    self.ListNhomHanghoa = ko.observableArray([]);

    // Danh sách h hàng hóa...
    self.ListGiaHanghoa = ko.observableArray([]); // Lưu tất cả GioHanghoa
    self.GiaHanghoaCurrent = ko.observable(null);

    //function
    self.CountSearch = ko.computed(function () {
        var i = 0;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.IsVisible() == true)
                i++;
        });
        return i;
    });

    self.InitData = function () {
        self.InitList();
    };
    self.InitList = function () {
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    index++;
                    return new Hanghoa(item.HanghoaId, item.Code, item.Ten, item.Giaban, item.NgayGiaban, item.NhomHanghoaId, item.Dangban, null);
                });
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/Hanghoa/GetHanghoaItems", 'application/json; charset=utf-8', 'get');

        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {
                $.each(data, function (key, value) {
                    self.ListNhomHanghoa.push(value);
                });
            }

            ko.utils.arrayForEach(self.List(), function (val1) {
                ko.utils.arrayForEach(self.ListNhomHanghoa(), function (val2) {
                    if (val1.NhomHanghoaId() == val2.NhomHanghoaId) {
                        val1.TenNhomHanghoa(val2.Ten);
                    }
                });

            });
        };

        AjaxInstance.Action("", "http://localhost:56173/NhomHanghoa/GetNhomHanghoaItems", 'application/json; charset=utf-8', 'get');

        // ~~ LẤY DANH SÁCH TẤT CẢ CÁC GIAHANGHOA ~~
        AjaxInstance.SuccessHandleFunction = function (data) {

            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    index++;
                    return new GiaHanghoa(item.GiaHanghoaId, item.Code, item.HanghoaId, item.Gia, item.NgayApdung);
                });
                self.ListGiaHanghoa.push.apply(self.ListGiaHanghoa, listItems);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/GiaHanghoa/GetGiaHanghoaItems", 'application/json; charset=utf-8', 'get');

    };

    //Action
    self.btnSetCurrent = function (item) {
        self.Thoat();
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);

    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.Ten().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
                if (iCurr == null) {
                    iCurr = item;
                    self.Current(item);
                }
                item.IsVisible(true);
            }
            else {
                item.IsVisible(false);
            }
        });
    };
    self.CreateHanghoa = function () {
        self.Thoat();
        var newItem = new Hanghoa(null, null, null, null, null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);
    };

    self.Thoat = function (item) {
        if (!item)
            item = self.Current();

        if (!item.OldValue())
            return;

        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.HanghoaId) {// Nếu không có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.HanghoaId(obj.HanghoaId);
            item.Code(obj.Code);
            item.Ten(obj.Ten);
            item.Giaban(obj.Giaban);
            item.NgayGiaban(obj.NgayGiaban);
            item.NhomHanghoaId(obj.NhomHanghoaId);
            item.Dangban(obj.Dangban)
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };

    self.Edit = function (item) {
        self.Thoat();
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.

         // Kiểm tra xem code có trùng không?
        //var codeTrung = false;
        //ko.utils.arrayForEach(self.List(), function (val) {
        //    if (val.Code() == item.Code() && val != item) {
        //        alert("Code \"" + val.Code() + "\" đã tồn tại cho hàng hóa \"" + val.Ten() + "\".");
        //        codeTrung = true;
        //        return;
        //    }
        //});
        //if (codeTrung)
        //    return;

        item.NhomHanghoaId($("#NhomHanghoaId").val());
        var jsonData = ko.toJSON(self.Current());

        if (self.Current().IsCreate() == false) {
            AjaxInstance.SuccessHandleFunction = function (data) {
                if (data == "success") {
                    item.TenNhomHanghoa($('#NhomHanghoaId option:selected').text());
                    self.Current().OldValue(null);
                    alert("Sửa thành công");
                } else {
                    alert(data);
                }
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/EditHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        else {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');

                if (arr[0] == "success") {
                    item.TenNhomHanghoa($('#NhomHanghoaId option:selected').text());
                    item.Dangban(false);
                    item.HanghoaId(arr[1]);
                    self.List.push(item);
                    alert("Thêm thành công.");
                } else {
                    alert(arr[0]);
                }
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/addHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };

    self.remove = function (item) {
        self.Thoat();
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.Ten()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                self.List.remove(item);
                alert("Xóa thành công");
            } else {
                alert(data);
            }
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/deleteHanghoaItem", 'application/json; charset=utf-8', 'post');

        self.Current(self.List()[0]);
    }

    self.batdauBan = function (item) {
        self.Thoat();
        if (confirm("Bạn có chắc chắn muốn bắt đầu bán món hàng: " + item.Ten() + " ?") == false) {
            return;
        }

        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                item.Dangban(true);
                alert("Món hàng " + item.Ten() + " đã được bắt đầu bán");
            } else {
                alert(data);
            }
            // alert(data);
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/batdauBan", 'application/json; charset=utf-8', 'post');
    }

    self.ngungBan = function (item) {
        self.Thoat();
        if (confirm("Bạn có chắc chắn muốn ngưng bán món hàng: " + item.Ten() + "?") == false) {
            return;
        }

        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                item.Dangban(false);
                alert("Món hàng " + item.Ten() + " đã bị ngưng bán.");
            }
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/ngungBan", 'application/json; charset=utf-8', 'post');
    }

    // ~~ GIÁ HÀNG HÓA ~~

    self.themGiaHanghoa = function (item) {
        var newItem = new GiaHanghoa(null, null, null, null, null);
        newItem.IsCreate(true);
        self.GiaHanghoaCurrent(newItem);
    }

    self.suaGiaHanghoa = function (item) {
        self.GiaHanghoaCurrent(item);
        self.GiaHanghoaCurrent().IsEdit(true);
        item.OldValue = ko.toJS(item);
        item.NgayApdung(item.getDateForInput()); // not local
    }

    self.xoaGiaHanghoa = function (item) {
        if (confirm("Bạn có chắc chắn muốn xóa") == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                // Kiểm tra xem giá háng hóa đã xóa có bằng với hàng hóa đang được áp dụng hay không?
                self.ListGiaHanghoa.remove(item);
                alert("Xóa thành công");
            }
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/deleteGiaHanghoaItem", 'application/json; charset=utf-8', 'post');

        self.Current(self.List()[0]);
    }

    self.luuGiaHanghoa = function (item) {
        try {
            if (!self.GiaHanghoaCurrent().isValid()) // Kiểm tra xem model này có đúng hay không
                return // Nếu sai thì return.
            if (self.GiaHanghoaCurrent().IsCreate()) {
                item.HanghoaId(self.Current().HanghoaId());
                var jsonData = ko.toJSON(item);
                // alert(ko.toJSON(item));
                AjaxInstance.SuccessHandleFunction = function (data) {
                    //alert(data);
                    var arr = data.split("-+-");
                    if (arr[0] == "success") {
                        item.GiaHanghoaId(arr[1]);
                        self.ListGiaHanghoa.push(item);
                        alert("Thêm giá hàng hóa thành công");
                        $('#myModal').modal('hide');
                    } else {
                        alert("Thêm thất bại");
                    }
                }
                AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/addGiaHanghoaItem", 'application/json; charset=utf-8', 'post');
            }
            else if (self.GiaHanghoaCurrent().IsEdit()) {
                var jsonData = ko.toJSON(item);
                alert(item.NgayApdung());
                //alert(jsonData);
                AjaxInstance.SuccessHandleFunction = function (data) {
                    //alert(data);
                    if (data == "success") {
                        alert("Sửa giá hàng hóa thành công");
                        $('#myModal').modal('hide');
                    } else {
                        alert("Sửa thất bại");
                    }
                }
                AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/editGiaHanghoaItem", 'application/json; charset=utf-8', 'post');

            }
        } catch (error) {
            alert(error.stack);
        }
    }

    self.huyGiaHanghoa = function (item) {
        try {
            var obj = JSON.parse(ko.toJSON(item.OldValue)); // Lấy oldValue
            if (obj.GiaHanghoaId) {// Nếu  có Id Nghĩa là người dùng ấn Edit rồi hũy   
                item.NgayApdung(obj.NgayApdung);
                item.Gia(obj.Gia);
                item.Code(obj.Code);
            }
        } catch (err) {
            alert(err.stack);
        }
    }
};

$(document).ready(function () {
    var model = new HanghoaViewModel();
    model.InitData();

    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
        grouping: { deep: true, observable: true }
    });
    ko.applyBindings(model);
});

