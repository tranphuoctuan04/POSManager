var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (AjaxInstance.SuccessHandleFunction != null) {
                    AjaxInstance.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert('Lỗi không thể kết nối đến server.');
            }
        });

    };
};
var AjaxInstance = new AjaxObj();

var GiaHanghoaViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");

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
            //alert(data);
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    //alert(item.co_name);
                    index++;
                    return new GiaHanghoa(item.GiaHanghoaId, item.Code, item.HanghoaId, item.Gia, item.NgayApdung);
                });
                // alert(listItems);
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/GiaHanghoa/GetGiaHanghoaItems", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.Code().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
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
        var newItem = new GiaHanghoa(null, null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);
    };

    self.Thoat = function (item) {
        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.GiaHanghoaId) {// Nếu không có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.GiaHanghoaId(obj.GiaHanghoaId); 
            item.Code(obj.Code); 
            item.HanghoaId(obj.HanghoaId); 
            item.Gia(obj.Gia); 
            item.NgayApdung(obj.NgayApdung) 
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };
    
    self.Edit = function(item){
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.

        var jsonData = ko.toJSON(self.Current());
        if (self.Current().IsCreate() == false)
        {
            AjaxInstance.SuccessHandleFunction = function (data) {
                alert(data);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/EditGiaHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        else
        {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');
                if (arr[0] == "Thêm thành công") {
                    item.GiaHanghoaId = arr[1];
                    self.List.push(item);
                }
                alert(arr[0]);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/addGiaHanghoaItem", 'application/json; charset=utf-8', 'post');
            
        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };
    self.remove = function(item)
    {
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.Code()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "Xóa thành công") self.List.remove(item);
            alert(data);        
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/GiaHanghoa/deleteGiaHanghoaItem", 'application/json; charset=utf-8', 'post');
        
        self.Current(self.List()[0]);
    }
};

$(document).ready(function () {
    var model = new GiaHanghoaViewModel();
    model.InitData();
    
    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
    });
    ko.applyBindings(model);
});

