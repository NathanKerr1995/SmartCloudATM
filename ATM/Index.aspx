<%@ Page Title="" Language="C#" MasterPageFile="~/ATM.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ATM.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
#outer-dropzone {
  height: 70%;
}

#inner-dropzone {
  height: 50%;
}

.drag-drop {
    content:url('Img/credit-card.png');
  display: inline-block;
}
</style>

<script>
    // target elements with the "draggable" class
    interact('.draggable')
        .draggable({
            // enable inertial throwing
            inertia: true,
            // keep the element within the area of it's parent
            modifiers: [
                interact.modifiers.restrictRect({
                    restriction: 'parent',
                    endOnly: true
                })
    ],
            // enable autoScroll
            autoScroll: true,

            // call this function on every dragmove event
            onmove: dragMoveListener,
            // call this function on every dragend event
            onend: function (event) {
                var textEl = event.target.querySelector('p')

                textEl && (textEl.textContent =
                    'moved a distance of ' +
                    (Math.sqrt(Math.pow(event.pageX - event.x0, 2) +
                        Math.pow(event.pageY - event.y0, 2) | 0))
                        .toFixed(2) + 'px')
            }
        });

function dragMoveListener (event) {
  var target = event.target
  // keep the dragged position in the data-x/data-y attributes
  var x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx
  var y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy

  // translate the element
  target.style.webkitTransform =
    target.style.transform =
      'translate(' + x + 'px, ' + y + 'px)'

  // update the posiion attributes
  target.setAttribute('data-x', x)
  target.setAttribute('data-y', y)
    };

// this is used later in the resizing and gesture demos
    window.dragMoveListener = dragMoveListener

    /* The dragging code for '.draggable' from the demo above
 * applies to this demo as well so it doesn't have to be repeated. */

// enable draggables to be dropped into this
    interact('.dropzone').dropzone({
        // only accept elements matching this CSS selector
        accept: '#yes-drop',
        // Require a 75% element overlap for a drop to be possible
        overlap: 0.75,

        // listen for drop related events:

        ondropactivate: function (event) {
            // add active dropzone feedback
            event.target.classList.add('drop-active')
        },
        ondragenter: function (event) {
            var draggableElement = event.relatedTarget
            var dropzoneElement = event.target

            // feedback the possibility of a drop
            dropzoneElement.classList.add('drop-target')
            draggableElement.classList.add('can-drop')
            draggableElement.textContent = 'Dragged in'
            document.getElementById('<%= lblAtmMessage.ClientID %>').textContent = "Release Card"
        },
        ondragleave: function (event) {
            // remove the drop feedback style
            event.target.classList.remove('drop-target')
            event.relatedTarget.classList.remove('can-drop')
            event.relatedTarget.textContent = 'Dragged out'
            document.getElementById('<%= lblAtmMessage.ClientID %>').textContent = "Enter your card"
        },
        ondrop: function (event) {
            event.relatedTarget.textContent = 'Dropped'
            location.href = 'Login.aspx'
        },
        ondropdeactivate: function (event) {
            // remove active dropzone feedback
            event.target.classList.remove('drop-active')
            event.target.classList.remove('drop-target')
        }
    });

    interact('.drag-drop')
        .draggable({
            inertia: true,
            modifiers: [
                interact.modifiers.restrictRect({
                    restriction: 'parent',
                    endOnly: true
                })
            ],
            autoScroll: true,
            // dragMoveListener from the dragging demo above
            onmove: dragMoveListener

        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-row">
        <div class="form-group col-md-12 text-center">
          <h3 class="text-uppercase text-white font-weight-bold"><asp:Label ID="lblAtmMessage" runat="server" Text="Enter your card"></asp:Label></h3>
        
            
            <div id="outer-dropzone" class="dropzone">
                <div id="inner-dropzone" class="dropzone">
                    <asp:Image ID="imgAtm" runat="server" ImageUrl="~/Img/atm.png" />
                </div>
            </div>
            <div id="yes-drop" class="drag-drop">
                <asp:Image ID="imgCreditCard" runat="server" ImageUrl="~/Img/credit-card.png" />
            </div>
        </div>
    </div>
</asp:Content>
