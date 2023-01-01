class Draggables {
    constructor() {
        this.dragged = null;
        this.index = null;
        this.list = null;
    }

    init() {
        let self = this;

        document.addEventListener("dragstart", (event) => {
            let target = event.target;
            if (target.nodeName !== 'DIV')
                target = $(target).closest('div.dropzone')[0];

            self.dragged = target;
            self.list = $('DIV.dropzone');
            self.index = self.list.index(self.dragged);

            event.dataTransfer.setDragImage(target, 0, 0);
        });

        document.addEventListener("dragover", (event) => {
            event.preventDefault();
        });

        document.addEventListener("drop", (event) => {
            let target = event.target;
            if (target.nodeName !== 'DIV')
                target = $(target).closest('div.dropzone')[0];

            if ($(target).hasClass("dropzone") && target.id !== self.dragged.id) {
                self.dragged.remove(self.dragged);
                let indexDrop = self.list.index(target);

                if (self.index > indexDrop) {
                    target.before(self.dragged);
                } else {
                    target.after(self.dragged);
                }
            }

            updateAllIndexes();
        });
    }
}