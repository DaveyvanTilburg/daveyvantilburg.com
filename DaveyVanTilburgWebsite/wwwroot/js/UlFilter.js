class filter {
    constructor () {
        this.registrations = {};
    }
    
    register(name, selector) {
        const self = this;
        self.registrations[name] = "none";
        
        $(selector).on("change", function (e) {
            e.preventDefault();

            self.registrations[name] = $(this).val();
            self.onEvent(self);

            return false;
        });
    }

    init() {
        $(".filterable")
            .hide()
            .show();
    }

    onEvent(self) {
        $(".filterable")
            .hide()
            .filter(function () {
                if (filter === "none")
                    return true;

                var isValid = true;
                for (var key in self.registrations) {
                    var value = self.registrations[key];
                    
                    if (isValid === true)
                        if (value !== "none")
                            isValid = $(this).attr(`data-${key}`) === value;
                }
                
                return isValid;
            }).closest(".filterable").show();
    }
}