function showDiv(id) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if( !myReference ) 
        window.alert('Nothing works in this browser'); 
    else
    {
        if(myReference.style) 
            myReference.style.visibility = 'visible'; 
        else 
        {
            if(myReference.visibility) 
                myReference.visibility = 'show'; 
            else
                window.alert('Nothing works in this browser'); 
        } 
    }
}

function hideDiv(id) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if(!myReference) 
        window.alert(id); 
    else
    {
        if( myReference.style ) 
        {
            myReference.style.visibility = 'hidden'; 
        }
        else 
        {
            if( myReference.visibility ) 
                myReference.visibility = 'hide'; 
            else
                window.alert('Nothing works in this browser'); return; 
        } 
    }
}

function setDivAt(_id,_left,_top) 
{
    var myReference = parent['Mainframe'].document.getElementById(_id);
    if( !myReference ) 
        window.alert('Nothing works in this browser'); 
    else
    {
        if(myReference.style)
        {
            myReference.style.top =  _top; 
            myReference.style.left = _left; 
        }
    }
}

function setDivSize(_id, _width, _height) {
    var myReference = parent['Mainframe'].document.getElementById(_id);
    if (!myReference)
        window.alert('Nothing works in this browser');
    else {
        if (myReference.style) {
            myReference.style.width = _width;
            myReference.style.height = _height;
        }
    }
}

function replaceText(_id,_text) 
{
    var myReference = parent['Mainframe'].document.getElementById(_id);
    if(!myReference) 
        window.alert(id); 
    else
    {
        var strText=JsObject.strReplaceText(myReference.innerHTML,_id,_text);
        if (JsObject.bGetResult())
        {
            if( myReference.insertAdjacentHTML) {
                myReference.innerHTML = '';
                myReference.insertAdjacentHTML('AfterBegin',strText);
            } else if( typeof( myReference.innerHTML ) != 'undefined' ) {
                myReference.innerHTML += theString;
            } else {
            }
        }
        else 
            myReference.innerHTML = strText;
    }
}

function getText(id) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if(!myReference) 
        window.alert(id); 
    else
    {
        JsObject.vGetText(id,myReference.value);
    }
}		

function replaceImage(id,fileName,_width,_height) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if(!myReference) 
        window.alert(id); 
    else
    {				
        myReference.src = fileName;
        if (_width>0)
            myReference.width = _width; 
        if (_height>0)
            myReference.height = _height; 
    }
}

function setEnabled(id,bVal) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if(!myReference) 
        window.alert(id); 
    else
        myReference.disabled = !bVal;
}
        
function getZoom()
{
    return screen.deviceXDPI/screen.logicalXDPI;
}

function setEnabled(id,bVal) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    if(!myReference) 
        window.alert(id); 
    else
        myReference.disabled = !bVal;
}

function setChecked(id,bVal) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    var myRefImg = parent['Mainframe'].document.getElementById(id+'_Image');
    if(!myReference) 
        window.alert(id); 
    else
    {
        if (bVal)
        {
            myReference.value = 'on';
            myRefImg.src = 'img/checked.gif';
        }
        else
        {
            myReference.value = 'off';
            myRefImg.src = 'img/unchecked.gif';
        }
    }
}

function getChecked(id) 
{
    var myReference = parent['Mainframe'].document.getElementById(id);
    var myRefImg = parent['Mainframe'].document.getElementById(id+'_Image');
    if(!myReference) 
        window.alert(id); 
    else
    {
        return (myReference.value == 'on');
    }
    return false;
}
