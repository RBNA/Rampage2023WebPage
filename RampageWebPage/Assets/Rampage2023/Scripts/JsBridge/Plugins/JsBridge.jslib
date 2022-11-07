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
    
  ChangeScrollByUnity: function (deltaScroll) {
    window.pageYOffset += deltaScroll;
  },
  
  scrollWindowsToNewPos: function (x, y) {
      if(isNaN(x) || isNaN(y)){
          console.logerror("NAN", x, y)
          return;
      } 
  
      var numX = Number(x);
      var numY = Number(y);
  
      uss.scrollTo(numX, numY, window,  () => console.log("dOnE"));   
      uss.stopScrolling();
  },  
  
  IsMobileBrowser: function () {
      return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
    },
});