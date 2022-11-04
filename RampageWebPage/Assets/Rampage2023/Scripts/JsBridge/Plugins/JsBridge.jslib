mergeInto(LibraryManager.library, {
  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(UTF8ToString(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },
  
  FireTrigger: function (triggerId) {
    alert("Trigger fired - ID: " + triggerId);
  },
  
  GetTopScrollOffset: function () {
    var headerContent = document.getElementById("header-content");
    return headerContent.offsetHeight;
  },
  
  CurrentScrollPosition: function () {
    return document.documentElement.scrollTop;
  },
  
  SendCurrentAnimationFrame: function (currentFrame) {
    var animationFrame = document.getElementById("current-animation-frame");
    
    if(animationFrame == null) {
     animationFrame = document.createElement("current-animation-frame");
     animationFrame.id = "current-animation-frame";
     document.body.appendChild(animationFrame);
     console.log("Variable with name 'current-animation-frame' created by Unity webgl app. You can access the current animation frame of the webgl canvas by this element on the property 'current-frame'");
    }
    
    animationFrame.setAttribute("current-frame", currentFrame);
  },  
});