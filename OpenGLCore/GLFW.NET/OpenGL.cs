using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GLFW
{
    public static unsafe class OpenGL
    {
        #region Delegates

        public delegate IntPtr GetProcAddressHandler(string funcName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCULLFACEPROC(int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBEGINPROC(int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENDPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEX3FPROC(float v1, float v2, float v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLOR3FPROC(float v1, float v2, float v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTMATRIXFPROC(float[] matrix);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMATRIXMODEPROC(uint projection);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLLOADIDENTITYPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLORTHOPROC(float left, float right, float bottom, float top, float nearVal, float farVal);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRUSTUMPROC(float left, float right, float bottom, float top, float nearVal, float farVal);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRONTFACEPROC(int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLHINTPROC(int target, int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLLINEWIDTHPROC(float width);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOINTSIZEPROC(float size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOLYGONMODEPROC(int face, int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSCISSORPROC(int x, int y, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERFPROC(int target, int pname, float param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERFVPROC(int target, int pname, /*const*/ float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERIPROC(int target, int pname, int param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERIVPROC(int target, int pname, /*const*/ int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXIMAGE1DPROC(int target, int level, int internalformat, int width, int border, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXIMAGE2DPROC(int target, int level, int internalformat, int width, int height, int border, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWBUFFERPROC(int buf);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARPROC(uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARCOLORPROC(float red, float green, float blue, float alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARSTENCILPROC(int s);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARDEPTHPROC(float depth);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILMASKPROC(uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORMASKPROC(bool red, bool green, bool blue, bool alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDEPTHMASKPROC(bool flag);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDISABLEPROC(int cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENABLEPROC(int cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFINISHPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFLUSHPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLENDFUNCPROC(int sfactor, int dfactor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLLOGICOPPROC(int opcode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILFUNCPROC(int func, int reference, uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILOPPROC(int fail, int zfail, int zpass);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDEPTHFUNCPROC(int func);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPIXELSTOREFPROC(int pname, float param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPIXELSTOREIPROC(int pname, int param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLREADBUFFERPROC(int src);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLREADPIXELSPROC(int x, int y, int width, int height, int format, int type, void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBOOLEANVPROC(int pname, bool* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETDOUBLEVPROC(int pname, float* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLGETERRORPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETFLOATVPROC(int pname, float* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETINTEGERVPROC(int pname, int* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* PFNGLGETSTRINGPROC(int name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXIMAGEPROC(int target, int level, int format, int type, void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXPARAMETERFVPROC(int target, int pname, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXPARAMETERIVPROC(int target, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXLEVELPARAMETERFVPROC(int target, int level, int pname, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXLEVELPARAMETERIVPROC(int target, int level, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISENABLEDPROC(int cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDEPTHRANGEPROC(float n, float f);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVIEWPORTPROC(int x, int y, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWARRAYSPROC(int mode, int first, int count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWELEMENTSPROC(int mode, int count, int type, /*const*/ void* indices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOLYGONOFFSETPROC(float factor, float units);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYTEXIMAGE1DPROC(int target, int level, int internalformat, int x, int y, int width, int border);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYTEXIMAGE2DPROC(int target, int level, int internalformat, int x, int y, int width, int height, int border);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYTEXSUBIMAGE1DPROC(int target, int level, int xoffset, int x, int y, int width);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYTEXSUBIMAGE2DPROC(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXSUBIMAGE1DPROC(int target, int level, int xoffset, int width, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXSUBIMAGE2DPROC(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDTEXTUREPROC(int target, uint texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETETEXTURESPROC(int n, /*const*/ uint* textures);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENTEXTURESPROC(int n, uint* textures);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISTEXTUREPROC(uint texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWRANGEELEMENTSPROC(int mode, uint start, uint end, int count, int type, /*const*/ void* indices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXIMAGE3DPROC(int target, int level, int internalformat, int width, int height, int depth, int border, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXSUBIMAGE3DPROC(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, int type, /*const*/ void* pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYTEXSUBIMAGE3DPROC(int target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLACTIVETEXTUREPROC(int texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLECOVERAGEPROC(float value, bool invert);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXIMAGE3DPROC(int target, int level, int internalformat, int width, int height, int depth, int border, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXIMAGE2DPROC(int target, int level, int internalformat, int width, int height, int border, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXIMAGE1DPROC(int target, int level, int internalformat, int width, int border, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXSUBIMAGE3DPROC(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXSUBIMAGE2DPROC(int target, int level, int xoffset, int yoffset, int width, int height, int format, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPRESSEDTEXSUBIMAGE1DPROC(int target, int level, int xoffset, int width, int format, int imageSize, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETCOMPRESSEDTEXIMAGEPROC(int target, int level, void* img);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLENDFUNCSEPARATEPROC(int sfactorRGB, int dfactorRGB, int sfactorAlpha, int dfactorAlpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTIDRAWARRAYSPROC(int mode, /*const*/ int* first, /*const*/ int* count, int drawCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTIDRAWELEMENTSPROC(int mode, /*const*/ int* count, int type, /*const*/ void*/*const*/* indices, int drawCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOINTPARAMETERFPROC(int pname, float param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOINTPARAMETERFVPROC(int pname, /*const*/ float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOINTPARAMETERIPROC(int pname, int param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPOINTPARAMETERIVPROC(int pname, /*const*/ int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLENDCOLORPROC(float red, float green, float blue, float alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLENDEQUATIONPROC(int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENQUERIESPROC(int n, uint* ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETEQUERIESPROC(int n, /*const*/ uint* ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISQUERYPROC(uint id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBEGINQUERYPROC(int target, uint id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENDQUERYPROC(int target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETQUERYIVPROC(int target, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETQUERYOBJECTIVPROC(uint id, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETQUERYOBJECTUIVPROC(uint id, int pname, uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDBUFFERPROC(int target, uint buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETEBUFFERSPROC(int n, /*const*/ uint* buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENBUFFERSPROC(int n, uint* buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISBUFFERPROC(uint buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBUFFERDATAPROC(int target, IntPtr size, /*const*/ void* data, int usage);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBUFFERSUBDATAPROC(int target, IntPtr offset, IntPtr size, /*const*/ void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBUFFERSUBDATAPROC(int target, IntPtr offset, IntPtr size, void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* PFNGLMAPBUFFERPROC(int target, int access);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLUNMAPBUFFERPROC(int target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBUFFERPARAMETERIVPROC(int target, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBUFFERPOINTERVPROC(int target, int pname, out IntPtr args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLENDEQUATIONSEPARATEPROC(int modeRGB, int modeAlpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWBUFFERSPROC(int n, /*const*/ int* bufs);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILOPSEPARATEPROC(int face, int sfail, int dpfail, int dppass);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILFUNCSEPARATEPROC(int face, int func, int reference, uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSTENCILMASKSEPARATEPROC(int face, uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLATTACHSHADERPROC(uint program, uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDATTRIBLOCATIONPROC(uint program, uint index, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOMPILESHADERPROC(uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint PFNGLCREATEPROGRAMPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint PFNGLCREATESHADERPROC(int type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETEPROGRAMPROC(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETESHADERPROC(uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDETACHSHADERPROC(uint program, uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDISABLEVERTEXATTRIBARRAYPROC(uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENABLEVERTEXATTRIBARRAYPROC(uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEATTRIBPROC(uint program, uint index, int bufSize, out int length, out int size, out int type, IntPtr name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEUNIFORMPROC(uint program, uint index, int bufSize, out int length, out int size, out int type, IntPtr name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETATTACHEDSHADERSPROC(uint program, int maxCount, int* count, uint* shaders);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLGETATTRIBLOCATIONPROC(uint program, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETPROGRAMIVPROC(uint program, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETPROGRAMINFOLOGPROC(uint program, int bufSize, int* length, byte* infoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSHADERIVPROC(uint shader, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSHADERINFOLOGPROC(uint shader, int bufSize, int* length, byte* infoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSHADERSOURCEPROC(uint shader, int bufSize, int* length, byte* source);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLGETUNIFORMLOCATIONPROC(uint program, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETUNIFORMFVPROC(uint program, int location, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETUNIFORMIVPROC(uint program, int location, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBDVPROC(uint index, int pname, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBFVPROC(uint index, int pname, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBIVPROC(uint index, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBPOINTERVPROC(uint index, int pname, out IntPtr pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISPROGRAMPROC(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISSHADERPROC(uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLLINKPROGRAMPROC(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSHADERSOURCEPROC(uint shader, int count, /*const*/ byte** str, /*const*/ int* length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUSEPROGRAMPROC(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1FPROC(int location, float v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2FPROC(int location, float v0, float v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3FPROC(int location, float v0, float v1, float v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4FPROC(int location, float v0, float v1, float v2, float v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1IPROC(int location, int v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2IPROC(int location, int v0, int v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3IPROC(int location, int v0, int v1, int v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4IPROC(int location, int v0, int v1, int v2, int v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1FVPROC(int location, int count, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2FVPROC(int location, int count, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3FVPROC(int location, int count, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4FVPROC(int location, int count, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1IVPROC(int location, int count, /*const*/ int* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2IVPROC(int location, int count, /*const*/ int* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3IVPROC(int location, int count, /*const*/ int* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4IVPROC(int location, int count, /*const*/ int* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX2FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX3FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX4FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVALIDATEPROGRAMPROC(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1DPROC(uint index, float x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1DVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1FPROC(uint index, float x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1FVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1SPROC(uint index, short x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB1SVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2DPROC(uint index, float x, float y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2DVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2FPROC(uint index, float x, float y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2FVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2SPROC(uint index, short x, short y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB2SVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3DPROC(uint index, float x, float y, float z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3DVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3FPROC(uint index, float x, float y, float z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3FVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3SPROC(uint index, short x, short y, short z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB3SVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NBVPROC(uint index, /*const*/ sbyte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NIVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NSVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NUBPROC(uint index, byte x, byte y, byte z, byte w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NUBVPROC(uint index, /*const*/ byte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NUIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4NUSVPROC(uint index, /*const*/ ushort* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4BVPROC(uint index, /*const*/ sbyte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4DPROC(uint index, float x, float y, float z, float w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4DVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4FPROC(uint index, float x, float y, float z, float w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4FVPROC(uint index, /*const*/ float* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4IVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4SPROC(uint index, short x, short y, short z, short w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4SVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4UBVPROC(uint index, /*const*/ byte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4UIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIB4USVPROC(uint index, /*const*/ ushort* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBPOINTERPROC(uint index, int size, int type, bool normalized, int stride, /*const*/ void* pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX2X3FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX3X2FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX2X4FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX4X2FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX3X4FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMMATRIX4X3FVPROC(int location, int count, bool transpose, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORMASKIPROC(uint index, bool r, bool g, bool b, bool a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBOOLEANI_VPROC(int target, uint index, bool* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETINTEGERI_VPROC(int target, uint index, int* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENABLEIPROC(int target, uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDISABLEIPROC(int target, uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISENABLEDIPROC(int target, uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBEGINTRANSFORMFEEDBACKPROC(int primitiveMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENDTRANSFORMFEEDBACKPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDBUFFERRANGEPROC(int target, uint index, uint buffer, IntPtr offset, IntPtr size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDBUFFERBASEPROC(int target, uint index, uint buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTRANSFORMFEEDBACKVARYINGSPROC(uint program, int count, /*const*/ byte** varyings, int bufferMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTRANSFORMFEEDBACKVARYINGPROC(uint program, uint index, int bufSize, out int length, out int size, out int type, IntPtr name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLAMPCOLORPROC(int target, int clamp);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBEGINCONDITIONALRENDERPROC(uint id, int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLENDCONDITIONALRENDERPROC();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBIPOINTERPROC(uint index, int size, int type, int stride, /*const*/ void* pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBIIVPROC(uint index, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETVERTEXATTRIBIUIVPROC(uint index, int pname, uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI1IPROC(uint index, int x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI2IPROC(uint index, int x, int y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI3IPROC(uint index, int x, int y, int z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4IPROC(uint index, int x, int y, int z, int w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI1UIPROC(uint index, uint x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI2UIPROC(uint index, uint x, uint y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI3UIPROC(uint index, uint x, uint y, uint z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4UIPROC(uint index, uint x, uint y, uint z, uint w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI1IVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI2IVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI3IVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4IVPROC(uint index, /*const*/ int* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI1UIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI2UIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI3UIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4UIVPROC(uint index, /*const*/ uint* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4BVPROC(uint index, /*const*/ sbyte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4SVPROC(uint index, /*const*/ short* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4UBVPROC(uint index, /*const*/ byte* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBI4USVPROC(uint index, /*const*/ ushort* v);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETUNIFORMUIVPROC(uint program, int location, uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDFRAGDATALOCATIONPROC(uint program, uint color, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLGETFRAGDATALOCATIONPROC(uint program, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1UIPROC(int location, uint v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2UIPROC(int location, uint v0, uint v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3UIPROC(int location, uint v0, uint v1, uint v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4UIPROC(int location, uint v0, uint v1, uint v2, uint v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM1UIVPROC(int location, int count, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM2UIVPROC(int location, int count, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM3UIVPROC(int location, int count, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORM4UIVPROC(int location, int count, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERIIVPROC(int target, int pname, /*const*/ int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXPARAMETERIUIVPROC(int target, int pname, /*const*/ uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXPARAMETERIIVPROC(int target, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETTEXPARAMETERIUIVPROC(int target, int pname, uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARBUFFERIVPROC(int buffer, int drawbuffer, /*const*/ int* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARBUFFERUIVPROC(int buffer, int drawbuffer, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARBUFFERFVPROC(int buffer, int drawbuffer, /*const*/ float* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCLEARBUFFERFIPROC(int buffer, int drawbuffer, float depth, int stencil);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* PFNGLGETSTRINGIPROC(int name, uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISRENDERBUFFERPROC(uint renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDRENDERBUFFERPROC(int target, uint renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETERENDERBUFFERSPROC(int n, /*const*/ uint* renderbuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENRENDERBUFFERSPROC(int n, uint* renderbuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLRENDERBUFFERSTORAGEPROC(int target, int internalformat, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETRENDERBUFFERPARAMETERIVPROC(int target, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISFRAMEBUFFERPROC(uint framebuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDFRAMEBUFFERPROC(int target, uint framebuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETEFRAMEBUFFERSPROC(int n, /*const*/ uint* framebuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENFRAMEBUFFERSPROC(int n, uint* framebuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLCHECKFRAMEBUFFERSTATUSPROC(int target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERTEXTURE1DPROC(int target, int attachment, int textarget, uint texture, int level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERTEXTURE2DPROC(int target, int attachment, int textarget, uint texture, int level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERTEXTURE3DPROC(int target, int attachment, int textarget, uint texture, int level, int zoffset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERRENDERBUFFERPROC(int target, int attachment, int renderbuffertarget, uint renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETFRAMEBUFFERATTACHMENTPARAMETERIVPROC(int target, int attachment, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENERATEMIPMAPPROC(int target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBLITFRAMEBUFFERPROC(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, int filter);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLRENDERBUFFERSTORAGEMULTISAMPLEPROC(int target, int samples, int internalformat, int width, int height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERTEXTURELAYERPROC(int target, int attachment, uint texture, int level, int layer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* PFNGLMAPBUFFERRANGEPROC(int target, IntPtr offset, IntPtr length, uint access);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFLUSHMAPPEDBUFFERRANGEPROC(int target, IntPtr offset, IntPtr length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDVERTEXARRAYPROC(uint array);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETEVERTEXARRAYSPROC(int n, /*const*/ uint* arrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENVERTEXARRAYSPROC(int n, uint* arrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISVERTEXARRAYPROC(uint array);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWARRAYSINSTANCEDPROC(int mode, int first, int count, int instanceCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWELEMENTSINSTANCEDPROC(int mode, int count, int type, /*const*/ void* indices, int instanceCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXBUFFERPROC(int target, int internalformat, uint buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPRIMITIVERESTARTINDEXPROC(uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOPYBUFFERSUBDATAPROC(int readTarget, int writeTarget, IntPtr readOffset, IntPtr writeOffset, IntPtr size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETUNIFORMINDICESPROC(uint program, int uniformCount, /*const*/ byte** uniformNames, uint* uniformIndices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEUNIFORMSIVPROC(uint program, int uniformCount, /*const*/ uint* uniformIndices, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEUNIFORMNAMEPROC(uint program, uint uniformIndex, int bufSize, int* length, byte* uniformName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint PFNGLGETUNIFORMBLOCKINDEXPROC(uint program, /*const*/ byte* uniformBlockName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEUNIFORMBLOCKIVPROC(uint program, uint uniformBlockIndex, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETACTIVEUNIFORMBLOCKNAMEPROC(uint program, uint uniformBlockIndex, int bufSize, int* length, byte* uniformBlockName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLUNIFORMBLOCKBINDINGPROC(uint program, uint uniformBlockIndex, uint uniformBlockBinding);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWELEMENTSBASEVERTEXPROC(int mode, int count, int type, /*const*/ void* indices, int baseVertex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWRANGEELEMENTSBASEVERTEXPROC(int mode, uint start, uint end, int count, int type, /*const*/ void* indices, int baseVertex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDRAWELEMENTSINSTANCEDBASEVERTEXPROC(int mode, int count, int type, /*const*/ void* indices, int instanceCount, int baseVertex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTIDRAWELEMENTSBASEVERTEXPROC(int mode, /*const*/ int* count, int type, /*const*/ void*/*const*/* indices, int drawCount, /*const*/ int* baseVertex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLPROVOKINGVERTEXPROC(int mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr PFNGLFENCESYNCPROC(int condition, uint flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISSYNCPROC(IntPtr sync);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETESYNCPROC(IntPtr sync);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLCLIENTWAITSYNCPROC(IntPtr sync, uint flags, ulong timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLWAITSYNCPROC(IntPtr sync, uint flags, ulong timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETINTEGER64VPROC(int pname, long* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSYNCIVPROC(IntPtr sync, int pname, int bufSize, int* length, int* values);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETINTEGER64I_VPROC(int target, uint index, long* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETBUFFERPARAMETERI64VPROC(int target, int pname, long* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLFRAMEBUFFERTEXTUREPROC(int target, int attachment, uint texture, int level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXIMAGE2DMULTISAMPLEPROC(int target, int samples, int internalformat, int width, int height, bool fixedsamplelocations);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXIMAGE3DMULTISAMPLEPROC(int target, int samples, int internalformat, int width, int height, int depth, bool fixedsamplelocations);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETMULTISAMPLEFVPROC(int pname, uint index, float* val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLEMASKIPROC(uint maskNumber, uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDFRAGDATALOCATIONINDEXEDPROC(uint program, uint colorNumber, uint index, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PFNGLGETFRAGDATAINDEXPROC(uint program, /*const*/ byte* name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGENSAMPLERSPROC(int count, uint* samplers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLDELETESAMPLERSPROC(int count, /*const*/ uint* samplers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool PFNGLISSAMPLERPROC(uint sampler);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLBINDSAMPLERPROC(uint unit, uint sampler);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERIPROC(uint sampler, int pname, int param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERIVPROC(uint sampler, int pname, /*const*/ int* param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERFPROC(uint sampler, int pname, float param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERFVPROC(uint sampler, int pname, /*const*/ float* param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERIIVPROC(uint sampler, int pname, /*const*/ int* param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSAMPLERPARAMETERIUIVPROC(uint sampler, int pname, /*const*/ uint* param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSAMPLERPARAMETERIVPROC(uint sampler, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSAMPLERPARAMETERIIVPROC(uint sampler, int pname, int* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSAMPLERPARAMETERFVPROC(uint sampler, int pname, float* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETSAMPLERPARAMETERIUIVPROC(uint sampler, int pname, uint* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLQUERYCOUNTERPROC(uint id, int target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETQUERYOBJECTI64VPROC(uint id, int pname, long* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLGETQUERYOBJECTUI64VPROC(uint id, int pname, ulong* args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBDIVISORPROC(uint index, uint divisor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP1UIPROC(uint index, int type, bool normalized, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP1UIVPROC(uint index, int type, bool normalized, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP2UIPROC(uint index, int type, bool normalized, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP2UIVPROC(uint index, int type, bool normalized, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP3UIPROC(uint index, int type, bool normalized, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP3UIVPROC(uint index, int type, bool normalized, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP4UIPROC(uint index, int type, bool normalized, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXATTRIBP4UIVPROC(uint index, int type, bool normalized, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP2UIPROC(int type, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP2UIVPROC(int type, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP3UIPROC(int type, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP3UIVPROC(int type, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP4UIPROC(int type, uint value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLVERTEXP4UIVPROC(int type, /*const*/ uint* value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP1UIPROC(int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP1UIVPROC(int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP2UIPROC(int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP2UIVPROC(int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP3UIPROC(int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP3UIVPROC(int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP4UIPROC(int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLTEXCOORDP4UIVPROC(int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP1UIPROC(int texture, int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP1UIVPROC(int texture, int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP2UIPROC(int texture, int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP2UIVPROC(int texture, int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP3UIPROC(int texture, int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP3UIVPROC(int texture, int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP4UIPROC(int texture, int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLMULTITEXCOORDP4UIVPROC(int texture, int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLNORMALP3UIPROC(int type, uint coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLNORMALP3UIVPROC(int type, /*const*/ uint* coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORP3UIPROC(int type, uint color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORP3UIVPROC(int type, /*const*/ uint* color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORP4UIPROC(int type, uint color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLCOLORP4UIVPROC(int type, /*const*/ uint* color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSECONDARYCOLORP3UIPROC(int type, uint color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PFNGLSECONDARYCOLORP3UIVPROC(int type, /*const*/ uint* color);

        #endregion Delegates

        #region Glfw function signatures

        private static PFNGLBEGINPROC _glBegin;
        private static PFNGLENDPROC _glEnd;
        private static PFNGLVERTEX3FPROC _glVertex3f;
        private static PFNGLCOLOR3FPROC _glColor3f;
        private static PFNGLMATRIXMODEPROC _glMatrixMode;
        private static PFNGLMULTMATRIXFPROC _glMultMatrixf;
        private static PFNGLLOADIDENTITYPROC _glLoadIdentity;
        private static PFNGLORTHOPROC _glOrtho;
        private static PFNGLFRUSTUMPROC _glFrustum;
        private static PFNGLCULLFACEPROC _glCullFace;
        private static PFNGLFRONTFACEPROC _glFrontFace;
        private static PFNGLHINTPROC _glHint;
        private static PFNGLLINEWIDTHPROC _glLineWidth;
        private static PFNGLPOINTSIZEPROC _glPointSize;
        private static PFNGLPOLYGONMODEPROC _glPolygonMode;
        private static PFNGLSCISSORPROC _glScissor;
        private static PFNGLTEXPARAMETERFPROC _glTexParameterf;
        private static PFNGLTEXPARAMETERFVPROC _glTexParameterfv;
        private static PFNGLTEXPARAMETERIPROC _glTexParameteri;
        private static PFNGLTEXPARAMETERIVPROC _glTexParameteriv;
        private static PFNGLTEXIMAGE1DPROC _glTexImage1D;
        private static PFNGLTEXIMAGE2DPROC _glTexImage2D;
        private static PFNGLDRAWBUFFERPROC _glDrawBuffer;
        private static PFNGLCLEARPROC _glClear;
        private static PFNGLCLEARCOLORPROC _glClearColor;
        private static PFNGLCLEARSTENCILPROC _glClearStencil;
        private static PFNGLCLEARDEPTHPROC _glClearDepth;
        private static PFNGLSTENCILMASKPROC _glStencilMask;
        private static PFNGLCOLORMASKPROC _glColorMask;
        private static PFNGLDEPTHMASKPROC _glDepthMask;
        private static PFNGLDISABLEPROC _glDisable;
        private static PFNGLENABLEPROC _glEnable;
        private static PFNGLFINISHPROC _glFinish;
        private static PFNGLFLUSHPROC _glFlush;
        private static PFNGLBLENDFUNCPROC _glBlendFunc;
        private static PFNGLLOGICOPPROC _glLogicOp;
        private static PFNGLSTENCILFUNCPROC _glStencilFunc;
        private static PFNGLSTENCILOPPROC _glStencilOp;
        private static PFNGLDEPTHFUNCPROC _glDepthFunc;
        private static PFNGLPIXELSTOREFPROC _glPixelStoref;
        private static PFNGLPIXELSTOREIPROC _glPixelStorei;
        private static PFNGLREADBUFFERPROC _glReadBuffer;
        private static PFNGLREADPIXELSPROC _glReadPixels;
        private static PFNGLGETBOOLEANVPROC _glGetBooleanv;
        private static PFNGLGETDOUBLEVPROC _glGetDoublev;
        private static PFNGLGETERRORPROC _glGetError;
        private static PFNGLGETFLOATVPROC _glGetFloatv;
        private static PFNGLGETINTEGERVPROC _glGetIntegerv;
        private static PFNGLGETSTRINGPROC _glGetString;
        private static PFNGLGETTEXIMAGEPROC _glGetTexImage;
        private static PFNGLGETTEXPARAMETERFVPROC _glGetTexParameterfv;
        private static PFNGLGETTEXPARAMETERIVPROC _glGetTexParameteriv;
        private static PFNGLGETTEXLEVELPARAMETERFVPROC _glGetTexLevelParameterfv;
        private static PFNGLGETTEXLEVELPARAMETERIVPROC _glGetTexLevelParameteriv;
        private static PFNGLISENABLEDPROC _glIsEnabled;
        private static PFNGLDEPTHRANGEPROC _glDepthRange;
        private static PFNGLVIEWPORTPROC _glViewport;
        private static PFNGLDRAWARRAYSPROC _glDrawArrays;
        private static PFNGLDRAWELEMENTSPROC _glDrawElements;
        private static PFNGLPOLYGONOFFSETPROC _glPolygonOffset;
        private static PFNGLCOPYTEXIMAGE1DPROC _glCopyTexImage1D;
        private static PFNGLCOPYTEXIMAGE2DPROC _glCopyTexImage2D;
        private static PFNGLCOPYTEXSUBIMAGE1DPROC _glCopyTexSubImage1D;
        private static PFNGLCOPYTEXSUBIMAGE2DPROC _glCopyTexSubImage2D;
        private static PFNGLTEXSUBIMAGE1DPROC _glTexSubImage1D;
        private static PFNGLTEXSUBIMAGE2DPROC _glTexSubImage2D;
        private static PFNGLBINDTEXTUREPROC _glBindTexture;
        private static PFNGLDELETETEXTURESPROC _glDeleteTextures;
        private static PFNGLGENTEXTURESPROC _glGenTextures;
        private static PFNGLISTEXTUREPROC _glIsTexture;
        private static PFNGLDRAWRANGEELEMENTSPROC _glDrawRangeElements;
        private static PFNGLTEXIMAGE3DPROC _glTexImage3D;
        private static PFNGLTEXSUBIMAGE3DPROC _glTexSubImage3D;
        private static PFNGLCOPYTEXSUBIMAGE3DPROC _glCopyTexSubImage3D;
        private static PFNGLACTIVETEXTUREPROC _glActiveTexture;
        private static PFNGLSAMPLECOVERAGEPROC _glSampleCoverage;
        private static PFNGLCOMPRESSEDTEXIMAGE3DPROC _glCompressedTexImage3D;
        private static PFNGLCOMPRESSEDTEXIMAGE2DPROC _glCompressedTexImage2D;
        private static PFNGLCOMPRESSEDTEXIMAGE1DPROC _glCompressedTexImage1D;
        private static PFNGLCOMPRESSEDTEXSUBIMAGE3DPROC _glCompressedTexSubImage3D;
        private static PFNGLCOMPRESSEDTEXSUBIMAGE2DPROC _glCompressedTexSubImage2D;
        private static PFNGLCOMPRESSEDTEXSUBIMAGE1DPROC _glCompressedTexSubImage1D;
        private static PFNGLGETCOMPRESSEDTEXIMAGEPROC _glGetCompressedTexImage;
        private static PFNGLBLENDFUNCSEPARATEPROC _glBlendFuncSeparate;
        private static PFNGLMULTIDRAWARRAYSPROC _glMultiDrawArrays;
        private static PFNGLMULTIDRAWELEMENTSPROC _glMultiDrawElements;
        private static PFNGLPOINTPARAMETERFPROC _glPointParameterf;
        private static PFNGLPOINTPARAMETERFVPROC _glPointParameterfv;
        private static PFNGLPOINTPARAMETERIPROC _glPointParameteri;
        private static PFNGLPOINTPARAMETERIVPROC _glPointParameteriv;
        private static PFNGLBLENDCOLORPROC _glBlendColor;
        private static PFNGLBLENDEQUATIONPROC _glBlendEquation;
        private static PFNGLGENQUERIESPROC _glGenQueries;
        private static PFNGLDELETEQUERIESPROC _glDeleteQueries;
        private static PFNGLISQUERYPROC _glIsQuery;
        private static PFNGLBEGINQUERYPROC _glBeginQuery;
        private static PFNGLENDQUERYPROC _glEndQuery;
        private static PFNGLGETQUERYIVPROC _glGetQueryiv;
        private static PFNGLGETQUERYOBJECTIVPROC _glGetQueryObjectiv;
        private static PFNGLGETQUERYOBJECTUIVPROC _glGetQueryObjectuiv;
        private static PFNGLBINDBUFFERPROC _glBindBuffer;
        private static PFNGLDELETEBUFFERSPROC _glDeleteBuffers;
        private static PFNGLGENBUFFERSPROC _glGenBuffers;
        private static PFNGLISBUFFERPROC _glIsBuffer;
        private static PFNGLBUFFERDATAPROC _glBufferData;
        private static PFNGLBUFFERSUBDATAPROC _glBufferSubData;
        private static PFNGLGETBUFFERSUBDATAPROC _glGetBufferSubData;
        private static PFNGLMAPBUFFERPROC _glMapBuffer;
        private static PFNGLUNMAPBUFFERPROC _glUnmapBuffer;
        private static PFNGLGETBUFFERPARAMETERIVPROC _glGetBufferParameteriv;
        private static PFNGLGETBUFFERPOINTERVPROC _glGetBufferPointerv;
        private static PFNGLBLENDEQUATIONSEPARATEPROC _glBlendEquationSeparate;
        private static PFNGLDRAWBUFFERSPROC _glDrawBuffers;
        private static PFNGLSTENCILOPSEPARATEPROC _glStencilOpSeparate;
        private static PFNGLSTENCILFUNCSEPARATEPROC _glStencilFuncSeparate;
        private static PFNGLSTENCILMASKSEPARATEPROC _glStencilMaskSeparate;
        private static PFNGLATTACHSHADERPROC _glAttachShader;
        private static PFNGLBINDATTRIBLOCATIONPROC _glBindAttribLocation;
        private static PFNGLCOMPILESHADERPROC _glCompileShader;
        private static PFNGLCREATEPROGRAMPROC _glCreateProgram;
        private static PFNGLCREATESHADERPROC _glCreateShader;
        private static PFNGLDELETEPROGRAMPROC _glDeleteProgram;
        private static PFNGLDELETESHADERPROC _glDeleteShader;
        private static PFNGLDETACHSHADERPROC _glDetachShader;
        private static PFNGLDISABLEVERTEXATTRIBARRAYPROC _glDisableVertexAttribArray;
        private static PFNGLENABLEVERTEXATTRIBARRAYPROC _glEnableVertexAttribArray;
        private static PFNGLGETACTIVEATTRIBPROC _glGetActiveAttrib;
        private static PFNGLGETACTIVEUNIFORMPROC _glGetActiveUniform;
        private static PFNGLGETATTACHEDSHADERSPROC _glGetAttachedShaders;
        private static PFNGLGETATTRIBLOCATIONPROC _glGetAttribLocation;
        private static PFNGLGETPROGRAMIVPROC _glGetProgramiv;
        private static PFNGLGETPROGRAMINFOLOGPROC _glGetProgramInfoLog;
        private static PFNGLGETSHADERIVPROC _glGetShaderiv;
        private static PFNGLGETSHADERINFOLOGPROC _glGetShaderInfoLog;
        private static PFNGLGETSHADERSOURCEPROC _glGetShaderSource;
        private static PFNGLGETUNIFORMLOCATIONPROC _glGetUniformLocation;
        private static PFNGLGETUNIFORMFVPROC _glGetUniformfv;
        private static PFNGLGETUNIFORMIVPROC _glGetUniformiv;
        private static PFNGLGETVERTEXATTRIBDVPROC _glGetVertexAttribdv;
        private static PFNGLGETVERTEXATTRIBFVPROC _glGetVertexAttribfv;
        private static PFNGLGETVERTEXATTRIBIVPROC _glGetVertexAttribiv;
        private static PFNGLGETVERTEXATTRIBPOINTERVPROC _glGetVertexAttribPointerv;
        private static PFNGLISPROGRAMPROC _glIsProgram;
        private static PFNGLISSHADERPROC _glIsShader;
        private static PFNGLLINKPROGRAMPROC _glLinkProgram;
        private static PFNGLSHADERSOURCEPROC _glShaderSource;
        private static PFNGLUSEPROGRAMPROC _glUseProgram;
        private static PFNGLUNIFORM1FPROC _glUniform1f;
        private static PFNGLUNIFORM2FPROC _glUniform2f;
        private static PFNGLUNIFORM3FPROC _glUniform3f;
        private static PFNGLUNIFORM4FPROC _glUniform4f;
        private static PFNGLUNIFORM1IPROC _glUniform1i;
        private static PFNGLUNIFORM2IPROC _glUniform2i;
        private static PFNGLUNIFORM3IPROC _glUniform3i;
        private static PFNGLUNIFORM4IPROC _glUniform4i;
        private static PFNGLUNIFORM1FVPROC _glUniform1fv;
        private static PFNGLUNIFORM2FVPROC _glUniform2fv;
        private static PFNGLUNIFORM3FVPROC _glUniform3fv;
        private static PFNGLUNIFORM4FVPROC _glUniform4fv;
        private static PFNGLUNIFORM1IVPROC _glUniform1iv;
        private static PFNGLUNIFORM2IVPROC _glUniform2iv;
        private static PFNGLUNIFORM3IVPROC _glUniform3iv;
        private static PFNGLUNIFORM4IVPROC _glUniform4iv;
        private static PFNGLUNIFORMMATRIX2FVPROC _glUniformMatrix2fv;
        private static PFNGLUNIFORMMATRIX3FVPROC _glUniformMatrix3fv;
        private static PFNGLUNIFORMMATRIX4FVPROC _glUniformMatrix4fv;
        private static PFNGLVALIDATEPROGRAMPROC _glValidateProgram;
        private static PFNGLVERTEXATTRIB1DPROC _glVertexAttrib1d;
        private static PFNGLVERTEXATTRIB1DVPROC _glVertexAttrib1dv;
        private static PFNGLVERTEXATTRIB1FPROC _glVertexAttrib1f;
        private static PFNGLVERTEXATTRIB1FVPROC _glVertexAttrib1fv;
        private static PFNGLVERTEXATTRIB1SPROC _glVertexAttrib1s;
        private static PFNGLVERTEXATTRIB1SVPROC _glVertexAttrib1sv;
        private static PFNGLVERTEXATTRIB2DPROC _glVertexAttrib2d;
        private static PFNGLVERTEXATTRIB2DVPROC _glVertexAttrib2dv;
        private static PFNGLVERTEXATTRIB2FPROC _glVertexAttrib2f;
        private static PFNGLVERTEXATTRIB2FVPROC _glVertexAttrib2fv;
        private static PFNGLVERTEXATTRIB2SPROC _glVertexAttrib2s;
        private static PFNGLVERTEXATTRIB2SVPROC _glVertexAttrib2sv;
        private static PFNGLVERTEXATTRIB3DPROC _glVertexAttrib3d;
        private static PFNGLVERTEXATTRIB3DVPROC _glVertexAttrib3dv;
        private static PFNGLVERTEXATTRIB3FPROC _glVertexAttrib3f;
        private static PFNGLVERTEXATTRIB3FVPROC _glVertexAttrib3fv;
        private static PFNGLVERTEXATTRIB3SPROC _glVertexAttrib3s;
        private static PFNGLVERTEXATTRIB3SVPROC _glVertexAttrib3sv;
        private static PFNGLVERTEXATTRIB4NBVPROC _glVertexAttrib4Nbv;
        private static PFNGLVERTEXATTRIB4NIVPROC _glVertexAttrib4Niv;
        private static PFNGLVERTEXATTRIB4NSVPROC _glVertexAttrib4Nsv;
        private static PFNGLVERTEXATTRIB4NUBPROC _glVertexAttrib4Nub;
        private static PFNGLVERTEXATTRIB4NUBVPROC _glVertexAttrib4Nubv;
        private static PFNGLVERTEXATTRIB4NUIVPROC _glVertexAttrib4Nuiv;
        private static PFNGLVERTEXATTRIB4NUSVPROC _glVertexAttrib4Nusv;
        private static PFNGLVERTEXATTRIB4BVPROC _glVertexAttrib4bv;
        private static PFNGLVERTEXATTRIB4DPROC _glVertexAttrib4d;
        private static PFNGLVERTEXATTRIB4DVPROC _glVertexAttrib4dv;
        private static PFNGLVERTEXATTRIB4FPROC _glVertexAttrib4f;
        private static PFNGLVERTEXATTRIB4FVPROC _glVertexAttrib4fv;
        private static PFNGLVERTEXATTRIB4IVPROC _glVertexAttrib4iv;
        private static PFNGLVERTEXATTRIB4SPROC _glVertexAttrib4s;
        private static PFNGLVERTEXATTRIB4SVPROC _glVertexAttrib4sv;
        private static PFNGLVERTEXATTRIB4UBVPROC _glVertexAttrib4ubv;
        private static PFNGLVERTEXATTRIB4UIVPROC _glVertexAttrib4uiv;
        private static PFNGLVERTEXATTRIB4USVPROC _glVertexAttrib4usv;
        private static PFNGLVERTEXATTRIBPOINTERPROC _glVertexAttribPointer;
        private static PFNGLUNIFORMMATRIX2X3FVPROC _glUniformMatrix2x3fv;
        private static PFNGLUNIFORMMATRIX3X2FVPROC _glUniformMatrix3x2fv;
        private static PFNGLUNIFORMMATRIX2X4FVPROC _glUniformMatrix2x4fv;
        private static PFNGLUNIFORMMATRIX4X2FVPROC _glUniformMatrix4x2fv;
        private static PFNGLUNIFORMMATRIX3X4FVPROC _glUniformMatrix3x4fv;
        private static PFNGLUNIFORMMATRIX4X3FVPROC _glUniformMatrix4x3fv;
        private static PFNGLCOLORMASKIPROC _glColorMaski;
        private static PFNGLGETBOOLEANI_VPROC _glGetBooleani_v;
        private static PFNGLENABLEIPROC _glEnablei;
        private static PFNGLDISABLEIPROC _glDisablei;
        private static PFNGLISENABLEDIPROC _glIsEnabledi;
        private static PFNGLBEGINTRANSFORMFEEDBACKPROC _glBeginTransformFeedback;
        private static PFNGLENDTRANSFORMFEEDBACKPROC _glEndTransformFeedback;
        private static PFNGLTRANSFORMFEEDBACKVARYINGSPROC _glTransformFeedbackVaryings;
        private static PFNGLGETTRANSFORMFEEDBACKVARYINGPROC _glGetTransformFeedbackVarying;
        private static PFNGLCLAMPCOLORPROC _glClampColor;
        private static PFNGLBEGINCONDITIONALRENDERPROC _glBeginConditionalRender;
        private static PFNGLENDCONDITIONALRENDERPROC _glEndConditionalRender;
        private static PFNGLVERTEXATTRIBIPOINTERPROC _glVertexAttribIPointer;
        private static PFNGLGETVERTEXATTRIBIIVPROC _glGetVertexAttribIiv;
        private static PFNGLGETVERTEXATTRIBIUIVPROC _glGetVertexAttribIuiv;
        private static PFNGLVERTEXATTRIBI1IPROC _glVertexAttribI1i;
        private static PFNGLVERTEXATTRIBI2IPROC _glVertexAttribI2i;
        private static PFNGLVERTEXATTRIBI3IPROC _glVertexAttribI3i;
        private static PFNGLVERTEXATTRIBI4IPROC _glVertexAttribI4i;
        private static PFNGLVERTEXATTRIBI1UIPROC _glVertexAttribI1ui;
        private static PFNGLVERTEXATTRIBI2UIPROC _glVertexAttribI2ui;
        private static PFNGLVERTEXATTRIBI3UIPROC _glVertexAttribI3ui;
        private static PFNGLVERTEXATTRIBI4UIPROC _glVertexAttribI4ui;
        private static PFNGLVERTEXATTRIBI1IVPROC _glVertexAttribI1iv;
        private static PFNGLVERTEXATTRIBI2IVPROC _glVertexAttribI2iv;
        private static PFNGLVERTEXATTRIBI3IVPROC _glVertexAttribI3iv;
        private static PFNGLVERTEXATTRIBI4IVPROC _glVertexAttribI4iv;
        private static PFNGLVERTEXATTRIBI1UIVPROC _glVertexAttribI1uiv;
        private static PFNGLVERTEXATTRIBI2UIVPROC _glVertexAttribI2uiv;
        private static PFNGLVERTEXATTRIBI3UIVPROC _glVertexAttribI3uiv;
        private static PFNGLVERTEXATTRIBI4UIVPROC _glVertexAttribI4uiv;
        private static PFNGLVERTEXATTRIBI4BVPROC _glVertexAttribI4bv;
        private static PFNGLVERTEXATTRIBI4SVPROC _glVertexAttribI4sv;
        private static PFNGLVERTEXATTRIBI4UBVPROC _glVertexAttribI4ubv;
        private static PFNGLVERTEXATTRIBI4USVPROC _glVertexAttribI4usv;
        private static PFNGLGETUNIFORMUIVPROC _glGetUniformuiv;
        private static PFNGLBINDFRAGDATALOCATIONPROC _glBindFragDataLocation;
        private static PFNGLGETFRAGDATALOCATIONPROC _glGetFragDataLocation;
        private static PFNGLUNIFORM1UIPROC _glUniform1ui;
        private static PFNGLUNIFORM2UIPROC _glUniform2ui;
        private static PFNGLUNIFORM3UIPROC _glUniform3ui;
        private static PFNGLUNIFORM4UIPROC _glUniform4ui;
        private static PFNGLUNIFORM1UIVPROC _glUniform1uiv;
        private static PFNGLUNIFORM2UIVPROC _glUniform2uiv;
        private static PFNGLUNIFORM3UIVPROC _glUniform3uiv;
        private static PFNGLUNIFORM4UIVPROC _glUniform4uiv;
        private static PFNGLTEXPARAMETERIIVPROC _glTexParameterIiv;
        private static PFNGLTEXPARAMETERIUIVPROC _glTexParameterIuiv;
        private static PFNGLGETTEXPARAMETERIIVPROC _glGetTexParameterIiv;
        private static PFNGLGETTEXPARAMETERIUIVPROC _glGetTexParameterIuiv;
        private static PFNGLCLEARBUFFERIVPROC _glClearBufferiv;
        private static PFNGLCLEARBUFFERUIVPROC _glClearBufferuiv;
        private static PFNGLCLEARBUFFERFVPROC _glClearBufferfv;
        private static PFNGLCLEARBUFFERFIPROC _glClearBufferfi;
        private static PFNGLGETSTRINGIPROC _glGetStringi;
        private static PFNGLISRENDERBUFFERPROC _glIsRenderbuffer;
        private static PFNGLBINDRENDERBUFFERPROC _glBindRenderbuffer;
        private static PFNGLDELETERENDERBUFFERSPROC _glDeleteRenderbuffers;
        private static PFNGLGENRENDERBUFFERSPROC _glGenRenderbuffers;
        private static PFNGLRENDERBUFFERSTORAGEPROC _glRenderbufferStorage;
        private static PFNGLGETRENDERBUFFERPARAMETERIVPROC _glGetRenderbufferParameteriv;
        private static PFNGLISFRAMEBUFFERPROC _glIsFramebuffer;
        private static PFNGLBINDFRAMEBUFFERPROC _glBindFramebuffer;
        private static PFNGLDELETEFRAMEBUFFERSPROC _glDeleteFramebuffers;
        private static PFNGLGENFRAMEBUFFERSPROC _glGenFramebuffers;
        private static PFNGLCHECKFRAMEBUFFERSTATUSPROC _glCheckFramebufferStatus;
        private static PFNGLFRAMEBUFFERTEXTURE1DPROC _glFramebufferTexture1D;
        private static PFNGLFRAMEBUFFERTEXTURE2DPROC _glFramebufferTexture2D;
        private static PFNGLFRAMEBUFFERTEXTURE3DPROC _glFramebufferTexture3D;
        private static PFNGLFRAMEBUFFERRENDERBUFFERPROC _glFramebufferRenderbuffer;
        private static PFNGLGETFRAMEBUFFERATTACHMENTPARAMETERIVPROC _glGetFramebufferAttachmentParameteriv;
        private static PFNGLGENERATEMIPMAPPROC _glGenerateMipmap;
        private static PFNGLBLITFRAMEBUFFERPROC _glBlitFramebuffer;
        private static PFNGLRENDERBUFFERSTORAGEMULTISAMPLEPROC _glRenderbufferStorageMultisample;
        private static PFNGLFRAMEBUFFERTEXTURELAYERPROC _glFramebufferTextureLayer;
        private static PFNGLMAPBUFFERRANGEPROC _glMapBufferRange;
        private static PFNGLFLUSHMAPPEDBUFFERRANGEPROC _glFlushMappedBufferRange;
        private static PFNGLBINDVERTEXARRAYPROC _glBindVertexArray;
        private static PFNGLDELETEVERTEXARRAYSPROC _glDeleteVertexArrays;
        private static PFNGLGENVERTEXARRAYSPROC _glGenVertexArrays;
        private static PFNGLISVERTEXARRAYPROC _glIsVertexArray;
        private static PFNGLDRAWARRAYSINSTANCEDPROC _glDrawArraysInstanced;
        private static PFNGLDRAWELEMENTSINSTANCEDPROC _glDrawElementsInstanced;
        private static PFNGLTEXBUFFERPROC _glTexBuffer;
        private static PFNGLPRIMITIVERESTARTINDEXPROC _glPrimitiveRestartIndex;
        private static PFNGLCOPYBUFFERSUBDATAPROC _glCopyBufferSubData;
        private static PFNGLGETUNIFORMINDICESPROC _glGetUniformIndices;
        private static PFNGLGETACTIVEUNIFORMSIVPROC _glGetActiveUniformsiv;
        private static PFNGLGETACTIVEUNIFORMNAMEPROC _glGetActiveUniformName;
        private static PFNGLGETUNIFORMBLOCKINDEXPROC _glGetUniformBlockIndex;
        private static PFNGLGETACTIVEUNIFORMBLOCKIVPROC _glGetActiveUniformBlockiv;
        private static PFNGLGETACTIVEUNIFORMBLOCKNAMEPROC _glGetActiveUniformBlockName;
        private static PFNGLUNIFORMBLOCKBINDINGPROC _glUniformBlockBinding;
        private static PFNGLBINDBUFFERRANGEPROC _glBindBufferRange;
        private static PFNGLBINDBUFFERBASEPROC _glBindBufferBase;
        private static PFNGLGETINTEGERI_VPROC _glGetIntegeri_v;
        private static PFNGLDRAWELEMENTSBASEVERTEXPROC _glDrawElementsBaseVertex;
        private static PFNGLDRAWRANGEELEMENTSBASEVERTEXPROC _glDrawRangeElementsBaseVertex;
        private static PFNGLDRAWELEMENTSINSTANCEDBASEVERTEXPROC _glDrawElementsInstancedBaseVertex;
        private static PFNGLMULTIDRAWELEMENTSBASEVERTEXPROC _glMultiDrawElementsBaseVertex;
        private static PFNGLPROVOKINGVERTEXPROC _glProvokingVertex;
        private static PFNGLFENCESYNCPROC _glFenceSync;
        private static PFNGLISSYNCPROC _glIsSync;
        private static PFNGLDELETESYNCPROC _glDeleteSync;
        private static PFNGLCLIENTWAITSYNCPROC _glClientWaitSync;
        private static PFNGLWAITSYNCPROC _glWaitSync;
        private static PFNGLGETINTEGER64VPROC _glGetInteger64v;
        private static PFNGLGETSYNCIVPROC _glGetSynciv;
        private static PFNGLGETINTEGER64I_VPROC _glGetInteger64i_v;
        private static PFNGLGETBUFFERPARAMETERI64VPROC _glGetBufferParameteri64v;
        private static PFNGLFRAMEBUFFERTEXTUREPROC _glFramebufferTexture;
        private static PFNGLTEXIMAGE2DMULTISAMPLEPROC _glTexImage2DMultisample;
        private static PFNGLTEXIMAGE3DMULTISAMPLEPROC _glTexImage3DMultisample;
        private static PFNGLGETMULTISAMPLEFVPROC _glGetMultisamplefv;
        private static PFNGLSAMPLEMASKIPROC _glSampleMaski;
        private static PFNGLBINDFRAGDATALOCATIONINDEXEDPROC _glBindFragDataLocationIndexed;
        private static PFNGLGETFRAGDATAINDEXPROC _glGetFragDataIndex;
        private static PFNGLGENSAMPLERSPROC _glGenSamplers;
        private static PFNGLDELETESAMPLERSPROC _glDeleteSamplers;
        private static PFNGLISSAMPLERPROC _glIsSampler;
        private static PFNGLBINDSAMPLERPROC _glBindSampler;
        private static PFNGLSAMPLERPARAMETERIPROC _glSamplerParameteri;
        private static PFNGLSAMPLERPARAMETERIVPROC _glSamplerParameteriv;
        private static PFNGLSAMPLERPARAMETERFPROC _glSamplerParameterf;
        private static PFNGLSAMPLERPARAMETERFVPROC _glSamplerParameterfv;
        private static PFNGLSAMPLERPARAMETERIIVPROC _glSamplerParameterIiv;
        private static PFNGLSAMPLERPARAMETERIUIVPROC _glSamplerParameterIuiv;
        private static PFNGLGETSAMPLERPARAMETERIVPROC _glGetSamplerParameteriv;
        private static PFNGLGETSAMPLERPARAMETERIIVPROC _glGetSamplerParameterIiv;
        private static PFNGLGETSAMPLERPARAMETERFVPROC _glGetSamplerParameterfv;
        private static PFNGLGETSAMPLERPARAMETERIUIVPROC _glGetSamplerParameterIuiv;
        private static PFNGLQUERYCOUNTERPROC _glQueryCounter;
        private static PFNGLGETQUERYOBJECTI64VPROC _glGetQueryObjecti64v;
        private static PFNGLGETQUERYOBJECTUI64VPROC _glGetQueryObjectui64v;
        private static PFNGLVERTEXATTRIBDIVISORPROC _glVertexAttribDivisor;
        private static PFNGLVERTEXATTRIBP1UIPROC _glVertexAttribP1ui;
        private static PFNGLVERTEXATTRIBP1UIVPROC _glVertexAttribP1uiv;
        private static PFNGLVERTEXATTRIBP2UIPROC _glVertexAttribP2ui;
        private static PFNGLVERTEXATTRIBP2UIVPROC _glVertexAttribP2uiv;
        private static PFNGLVERTEXATTRIBP3UIPROC _glVertexAttribP3ui;
        private static PFNGLVERTEXATTRIBP3UIVPROC _glVertexAttribP3uiv;
        private static PFNGLVERTEXATTRIBP4UIPROC _glVertexAttribP4ui;
        private static PFNGLVERTEXATTRIBP4UIVPROC _glVertexAttribP4uiv;
        private static PFNGLVERTEXP2UIPROC _glVertexP2ui;
        private static PFNGLVERTEXP2UIVPROC _glVertexP2uiv;
        private static PFNGLVERTEXP3UIPROC _glVertexP3ui;
        private static PFNGLVERTEXP3UIVPROC _glVertexP3uiv;
        private static PFNGLVERTEXP4UIPROC _glVertexP4ui;
        private static PFNGLVERTEXP4UIVPROC _glVertexP4uiv;
        private static PFNGLTEXCOORDP1UIPROC _glTexCoordP1ui;
        private static PFNGLTEXCOORDP1UIVPROC _glTexCoordP1uiv;
        private static PFNGLTEXCOORDP2UIPROC _glTexCoordP2ui;
        private static PFNGLTEXCOORDP2UIVPROC _glTexCoordP2uiv;
        private static PFNGLTEXCOORDP3UIPROC _glTexCoordP3ui;
        private static PFNGLTEXCOORDP3UIVPROC _glTexCoordP3uiv;
        private static PFNGLTEXCOORDP4UIPROC _glTexCoordP4ui;
        private static PFNGLTEXCOORDP4UIVPROC _glTexCoordP4uiv;
        private static PFNGLMULTITEXCOORDP1UIPROC _glMultiTexCoordP1ui;
        private static PFNGLMULTITEXCOORDP1UIVPROC _glMultiTexCoordP1uiv;
        private static PFNGLMULTITEXCOORDP2UIPROC _glMultiTexCoordP2ui;
        private static PFNGLMULTITEXCOORDP2UIVPROC _glMultiTexCoordP2uiv;
        private static PFNGLMULTITEXCOORDP3UIPROC _glMultiTexCoordP3ui;
        private static PFNGLMULTITEXCOORDP3UIVPROC _glMultiTexCoordP3uiv;
        private static PFNGLMULTITEXCOORDP4UIPROC _glMultiTexCoordP4ui;
        private static PFNGLMULTITEXCOORDP4UIVPROC _glMultiTexCoordP4uiv;
        private static PFNGLNORMALP3UIPROC _glNormalP3ui;
        private static PFNGLNORMALP3UIVPROC _glNormalP3uiv;
        private static PFNGLCOLORP3UIPROC _glColorP3ui;
        private static PFNGLCOLORP3UIVPROC _glColorP3uiv;
        private static PFNGLCOLORP4UIPROC _glColorP4ui;
        private static PFNGLCOLORP4UIVPROC _glColorP4uiv;
        private static PFNGLSECONDARYCOLORP3UIPROC _glSecondaryColorP3ui;
        private static PFNGLSECONDARYCOLORP3UIVPROC _glSecondaryColorP3uiv;

        #endregion Glfw function signatures

        #region Private logic

        private static string PtrToStringUtf8(IntPtr ptr)
        {
            var length = 0;
            while (Marshal.ReadByte(ptr, length) != 0)
                length++;
            var buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }

        private static string PtrToStringUtf8(IntPtr ptr, int length)
        {
            var buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }

        public static readonly void* NULL = (void*)0;

        public static void Load(GetProcAddressHandler loader)
        {
            _glMatrixMode = Marshal.GetDelegateForFunctionPointer<PFNGLMATRIXMODEPROC>(loader.Invoke("glMatrixMode"));
            _glBegin = Marshal.GetDelegateForFunctionPointer<PFNGLBEGINPROC>(loader.Invoke("glBegin"));
            _glEnd = Marshal.GetDelegateForFunctionPointer<PFNGLENDPROC>(loader.Invoke("glEnd"));
            _glVertex3f = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEX3FPROC>(loader.Invoke("glVertex3f"));
            _glColor3f = Marshal.GetDelegateForFunctionPointer<PFNGLCOLOR3FPROC>(loader.Invoke("glColor3f"));
            _glMultMatrixf = Marshal.GetDelegateForFunctionPointer<PFNGLMULTMATRIXFPROC>(loader.Invoke("glMultMatrixf"));
            _glLoadIdentity = Marshal.GetDelegateForFunctionPointer<PFNGLLOADIDENTITYPROC>(loader.Invoke("glLoadIdentity"));
            _glOrtho = Marshal.GetDelegateForFunctionPointer<PFNGLORTHOPROC>(loader.Invoke("glOrtho"));
            _glFrustum = Marshal.GetDelegateForFunctionPointer<PFNGLFRUSTUMPROC>(loader.Invoke("glFrustum"));
            _glCullFace = Marshal.GetDelegateForFunctionPointer<PFNGLCULLFACEPROC>(loader.Invoke("glCullFace"));
            _glFrontFace = Marshal.GetDelegateForFunctionPointer<PFNGLFRONTFACEPROC>(loader.Invoke("glFrontFace"));
            _glHint = Marshal.GetDelegateForFunctionPointer<PFNGLHINTPROC>(loader.Invoke("glHint"));
            _glLineWidth = Marshal.GetDelegateForFunctionPointer<PFNGLLINEWIDTHPROC>(loader.Invoke("glLineWidth"));
            _glPointSize = Marshal.GetDelegateForFunctionPointer<PFNGLPOINTSIZEPROC>(loader.Invoke("glPointSize"));
            _glPolygonMode = Marshal.GetDelegateForFunctionPointer<PFNGLPOLYGONMODEPROC>(loader.Invoke("glPolygonMode"));
            _glScissor = Marshal.GetDelegateForFunctionPointer<PFNGLSCISSORPROC>(loader.Invoke("glScissor"));
            _glTexParameterf = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERFPROC>(loader.Invoke("glTexParameterf"));
            _glTexParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERFVPROC>(loader.Invoke("glTexParameterfv"));
            _glTexParameteri = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERIPROC>(loader.Invoke("glTexParameteri"));
            _glTexParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERIVPROC>(loader.Invoke("glTexParameteriv"));
            _glTexImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXIMAGE1DPROC>(loader.Invoke("glTexImage1D"));
            _glTexImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXIMAGE2DPROC>(loader.Invoke("glTexImage2D"));
            _glDrawBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWBUFFERPROC>(loader.Invoke("glDrawBuffer"));
            _glClear = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARPROC>(loader.Invoke("glClear"));
            _glClearColor = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARCOLORPROC>(loader.Invoke("glClearColor"));
            _glClearStencil = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARSTENCILPROC>(loader.Invoke("glClearStencil"));
            _glClearDepth = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARDEPTHPROC>(loader.Invoke("glClearDepth"));
            _glStencilMask = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILMASKPROC>(loader.Invoke("glStencilMask"));
            _glColorMask = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORMASKPROC>(loader.Invoke("glColorMask"));
            _glDepthMask = Marshal.GetDelegateForFunctionPointer<PFNGLDEPTHMASKPROC>(loader.Invoke("glDepthMask"));
            _glDisable = Marshal.GetDelegateForFunctionPointer<PFNGLDISABLEPROC>(loader.Invoke("glDisable"));
            _glEnable = Marshal.GetDelegateForFunctionPointer<PFNGLENABLEPROC>(loader.Invoke("glEnable"));
            _glFinish = Marshal.GetDelegateForFunctionPointer<PFNGLFINISHPROC>(loader.Invoke("glFinish"));
            _glFlush = Marshal.GetDelegateForFunctionPointer<PFNGLFLUSHPROC>(loader.Invoke("glFlush"));
            _glBlendFunc = Marshal.GetDelegateForFunctionPointer<PFNGLBLENDFUNCPROC>(loader.Invoke("glBlendFunc"));
            _glLogicOp = Marshal.GetDelegateForFunctionPointer<PFNGLLOGICOPPROC>(loader.Invoke("glLogicOp"));
            _glStencilFunc = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILFUNCPROC>(loader.Invoke("glStencilFunc"));
            _glStencilOp = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILOPPROC>(loader.Invoke("glStencilOp"));
            _glDepthFunc = Marshal.GetDelegateForFunctionPointer<PFNGLDEPTHFUNCPROC>(loader.Invoke("glDepthFunc"));
            _glPixelStoref = Marshal.GetDelegateForFunctionPointer<PFNGLPIXELSTOREFPROC>(loader.Invoke("glPixelStoref"));
            _glPixelStorei = Marshal.GetDelegateForFunctionPointer<PFNGLPIXELSTOREIPROC>(loader.Invoke("glPixelStorei"));
            _glReadBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLREADBUFFERPROC>(loader.Invoke("glReadBuffer"));
            _glReadPixels = Marshal.GetDelegateForFunctionPointer<PFNGLREADPIXELSPROC>(loader.Invoke("glReadPixels"));
            _glGetBooleanv = Marshal.GetDelegateForFunctionPointer<PFNGLGETBOOLEANVPROC>(loader.Invoke("glGetBooleanv"));
            _glGetDoublev = Marshal.GetDelegateForFunctionPointer<PFNGLGETDOUBLEVPROC>(loader.Invoke("glGetDoublev"));
            _glGetError = Marshal.GetDelegateForFunctionPointer<PFNGLGETERRORPROC>(loader.Invoke("glGetError"));
            _glGetFloatv = Marshal.GetDelegateForFunctionPointer<PFNGLGETFLOATVPROC>(loader.Invoke("glGetFloatv"));
            _glGetIntegerv = Marshal.GetDelegateForFunctionPointer<PFNGLGETINTEGERVPROC>(loader.Invoke("glGetIntegerv"));
            _glGetString = Marshal.GetDelegateForFunctionPointer<PFNGLGETSTRINGPROC>(loader.Invoke("glGetString"));
            _glGetTexImage = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXIMAGEPROC>(loader.Invoke("glGetTexImage"));
            _glGetTexParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXPARAMETERFVPROC>(loader.Invoke("glGetTexParameterfv"));
            _glGetTexParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXPARAMETERIVPROC>(loader.Invoke("glGetTexParameteriv"));
            _glGetTexLevelParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXLEVELPARAMETERFVPROC>(loader.Invoke("glGetTexLevelParameterfv"));
            _glGetTexLevelParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXLEVELPARAMETERIVPROC>(loader.Invoke("glGetTexLevelParameteriv"));
            _glIsEnabled = Marshal.GetDelegateForFunctionPointer<PFNGLISENABLEDPROC>(loader.Invoke("glIsEnabled"));
            _glDepthRange = Marshal.GetDelegateForFunctionPointer<PFNGLDEPTHRANGEPROC>(loader.Invoke("glDepthRange"));
            _glViewport = Marshal.GetDelegateForFunctionPointer<PFNGLVIEWPORTPROC>(loader.Invoke("glViewport"));
            _glDrawArrays = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWARRAYSPROC>(loader.Invoke("glDrawArrays"));
            _glDrawElements = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWELEMENTSPROC>(loader.Invoke("glDrawElements"));
            _glPolygonOffset = Marshal.GetDelegateForFunctionPointer<PFNGLPOLYGONOFFSETPROC>(loader.Invoke("glPolygonOffset"));
            _glCopyTexImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYTEXIMAGE1DPROC>(loader.Invoke("glCopyTexImage1D"));
            _glCopyTexImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYTEXIMAGE2DPROC>(loader.Invoke("glCopyTexImage2D"));
            _glCopyTexSubImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYTEXSUBIMAGE1DPROC>(loader.Invoke("glCopyTexSubImage1D"));
            _glCopyTexSubImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYTEXSUBIMAGE2DPROC>(loader.Invoke("glCopyTexSubImage2D"));
            _glTexSubImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXSUBIMAGE1DPROC>(loader.Invoke("glTexSubImage1D"));
            _glTexSubImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXSUBIMAGE2DPROC>(loader.Invoke("glTexSubImage2D"));
            _glBindTexture = Marshal.GetDelegateForFunctionPointer<PFNGLBINDTEXTUREPROC>(loader.Invoke("glBindTexture"));
            _glDeleteTextures = Marshal.GetDelegateForFunctionPointer<PFNGLDELETETEXTURESPROC>(loader.Invoke("glDeleteTextures"));
            _glGenTextures = Marshal.GetDelegateForFunctionPointer<PFNGLGENTEXTURESPROC>(loader.Invoke("glGenTextures"));
            _glIsTexture = Marshal.GetDelegateForFunctionPointer<PFNGLISTEXTUREPROC>(loader.Invoke("glIsTexture"));
            _glDrawRangeElements = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWRANGEELEMENTSPROC>(loader.Invoke("glDrawRangeElements"));
            _glTexImage3D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXIMAGE3DPROC>(loader.Invoke("glTexImage3D"));
            _glTexSubImage3D = Marshal.GetDelegateForFunctionPointer<PFNGLTEXSUBIMAGE3DPROC>(loader.Invoke("glTexSubImage3D"));
            _glCopyTexSubImage3D = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYTEXSUBIMAGE3DPROC>(loader.Invoke("glCopyTexSubImage3D"));
            _glActiveTexture = Marshal.GetDelegateForFunctionPointer<PFNGLACTIVETEXTUREPROC>(loader.Invoke("glActiveTexture"));
            _glSampleCoverage = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLECOVERAGEPROC>(loader.Invoke("glSampleCoverage"));
            _glCompressedTexImage3D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXIMAGE3DPROC>(loader.Invoke("glCompressedTexImage3D"));
            _glCompressedTexImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXIMAGE2DPROC>(loader.Invoke("glCompressedTexImage2D"));
            _glCompressedTexImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXIMAGE1DPROC>(loader.Invoke("glCompressedTexImage1D"));
            _glCompressedTexSubImage3D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXSUBIMAGE3DPROC>(loader.Invoke("glCompressedTexSubImage3D"));
            _glCompressedTexSubImage2D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXSUBIMAGE2DPROC>(loader.Invoke("glCompressedTexSubImage2D"));
            _glCompressedTexSubImage1D = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPRESSEDTEXSUBIMAGE1DPROC>(loader.Invoke("glCompressedTexSubImage1D"));
            _glGetCompressedTexImage = Marshal.GetDelegateForFunctionPointer<PFNGLGETCOMPRESSEDTEXIMAGEPROC>(loader.Invoke("glGetCompressedTexImage"));
            _glBlendFuncSeparate = Marshal.GetDelegateForFunctionPointer<PFNGLBLENDFUNCSEPARATEPROC>(loader.Invoke("glBlendFuncSeparate"));
            _glMultiDrawArrays = Marshal.GetDelegateForFunctionPointer<PFNGLMULTIDRAWARRAYSPROC>(loader.Invoke("glMultiDrawArrays"));
            _glMultiDrawElements = Marshal.GetDelegateForFunctionPointer<PFNGLMULTIDRAWELEMENTSPROC>(loader.Invoke("glMultiDrawElements"));
            _glPointParameterf = Marshal.GetDelegateForFunctionPointer<PFNGLPOINTPARAMETERFPROC>(loader.Invoke("glPointParameterf"));
            _glPointParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLPOINTPARAMETERFVPROC>(loader.Invoke("glPointParameterfv"));
            _glPointParameteri = Marshal.GetDelegateForFunctionPointer<PFNGLPOINTPARAMETERIPROC>(loader.Invoke("glPointParameteri"));
            _glPointParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLPOINTPARAMETERIVPROC>(loader.Invoke("glPointParameteriv"));
            _glBlendColor = Marshal.GetDelegateForFunctionPointer<PFNGLBLENDCOLORPROC>(loader.Invoke("glBlendColor"));
            _glBlendEquation = Marshal.GetDelegateForFunctionPointer<PFNGLBLENDEQUATIONPROC>(loader.Invoke("glBlendEquation"));
            _glGenQueries = Marshal.GetDelegateForFunctionPointer<PFNGLGENQUERIESPROC>(loader.Invoke("glGenQueries"));
            _glDeleteQueries = Marshal.GetDelegateForFunctionPointer<PFNGLDELETEQUERIESPROC>(loader.Invoke("glDeleteQueries"));
            _glIsQuery = Marshal.GetDelegateForFunctionPointer<PFNGLISQUERYPROC>(loader.Invoke("glIsQuery"));
            _glBeginQuery = Marshal.GetDelegateForFunctionPointer<PFNGLBEGINQUERYPROC>(loader.Invoke("glBeginQuery"));
            _glEndQuery = Marshal.GetDelegateForFunctionPointer<PFNGLENDQUERYPROC>(loader.Invoke("glEndQuery"));
            _glGetQueryiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETQUERYIVPROC>(loader.Invoke("glGetQueryiv"));
            _glGetQueryObjectiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETQUERYOBJECTIVPROC>(loader.Invoke("glGetQueryObjectiv"));
            _glGetQueryObjectuiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETQUERYOBJECTUIVPROC>(loader.Invoke("glGetQueryObjectuiv"));
            _glBindBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLBINDBUFFERPROC>(loader.Invoke("glBindBuffer"));
            _glDeleteBuffers = Marshal.GetDelegateForFunctionPointer<PFNGLDELETEBUFFERSPROC>(loader.Invoke("glDeleteBuffers"));
            _glGenBuffers = Marshal.GetDelegateForFunctionPointer<PFNGLGENBUFFERSPROC>(loader.Invoke("glGenBuffers"));
            _glIsBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLISBUFFERPROC>(loader.Invoke("glIsBuffer"));
            _glBufferData = Marshal.GetDelegateForFunctionPointer<PFNGLBUFFERDATAPROC>(loader.Invoke("glBufferData"));
            _glBufferSubData = Marshal.GetDelegateForFunctionPointer<PFNGLBUFFERSUBDATAPROC>(loader.Invoke("glBufferSubData"));
            _glGetBufferSubData = Marshal.GetDelegateForFunctionPointer<PFNGLGETBUFFERSUBDATAPROC>(loader.Invoke("glGetBufferSubData"));
            _glMapBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLMAPBUFFERPROC>(loader.Invoke("glMapBuffer"));
            _glUnmapBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLUNMAPBUFFERPROC>(loader.Invoke("glUnmapBuffer"));
            _glGetBufferParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETBUFFERPARAMETERIVPROC>(loader.Invoke("glGetBufferParameteriv"));
            _glGetBufferPointerv = Marshal.GetDelegateForFunctionPointer<PFNGLGETBUFFERPOINTERVPROC>(loader.Invoke("glGetBufferPointerv"));
            _glBlendEquationSeparate = Marshal.GetDelegateForFunctionPointer<PFNGLBLENDEQUATIONSEPARATEPROC>(loader.Invoke("glBlendEquationSeparate"));
            _glDrawBuffers = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWBUFFERSPROC>(loader.Invoke("glDrawBuffers"));
            _glStencilOpSeparate = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILOPSEPARATEPROC>(loader.Invoke("glStencilOpSeparate"));
            _glStencilFuncSeparate = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILFUNCSEPARATEPROC>(loader.Invoke("glStencilFuncSeparate"));
            _glStencilMaskSeparate = Marshal.GetDelegateForFunctionPointer<PFNGLSTENCILMASKSEPARATEPROC>(loader.Invoke("glStencilMaskSeparate"));
            _glAttachShader = Marshal.GetDelegateForFunctionPointer<PFNGLATTACHSHADERPROC>(loader.Invoke("glAttachShader"));
            _glBindAttribLocation = Marshal.GetDelegateForFunctionPointer<PFNGLBINDATTRIBLOCATIONPROC>(loader.Invoke("glBindAttribLocation"));
            _glCompileShader = Marshal.GetDelegateForFunctionPointer<PFNGLCOMPILESHADERPROC>(loader.Invoke("glCompileShader"));
            _glCreateProgram = Marshal.GetDelegateForFunctionPointer<PFNGLCREATEPROGRAMPROC>(loader.Invoke("glCreateProgram"));
            _glCreateShader = Marshal.GetDelegateForFunctionPointer<PFNGLCREATESHADERPROC>(loader.Invoke("glCreateShader"));
            _glDeleteProgram = Marshal.GetDelegateForFunctionPointer<PFNGLDELETEPROGRAMPROC>(loader.Invoke("glDeleteProgram"));
            _glDeleteShader = Marshal.GetDelegateForFunctionPointer<PFNGLDELETESHADERPROC>(loader.Invoke("glDeleteShader"));
            _glDetachShader = Marshal.GetDelegateForFunctionPointer<PFNGLDETACHSHADERPROC>(loader.Invoke("glDetachShader"));
            _glDisableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<PFNGLDISABLEVERTEXATTRIBARRAYPROC>(loader.Invoke("glDisableVertexAttribArray"));
            _glEnableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<PFNGLENABLEVERTEXATTRIBARRAYPROC>(loader.Invoke("glEnableVertexAttribArray"));
            _glGetActiveAttrib = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEATTRIBPROC>(loader.Invoke("glGetActiveAttrib"));
            _glGetActiveUniform = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEUNIFORMPROC>(loader.Invoke("glGetActiveUniform"));
            _glGetAttachedShaders = Marshal.GetDelegateForFunctionPointer<PFNGLGETATTACHEDSHADERSPROC>(loader.Invoke("glGetAttachedShaders"));
            _glGetAttribLocation = Marshal.GetDelegateForFunctionPointer<PFNGLGETATTRIBLOCATIONPROC>(loader.Invoke("glGetAttribLocation"));
            _glGetProgramiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETPROGRAMIVPROC>(loader.Invoke("glGetProgramiv"));
            _glGetProgramInfoLog = Marshal.GetDelegateForFunctionPointer<PFNGLGETPROGRAMINFOLOGPROC>(loader.Invoke("glGetProgramInfoLog"));
            _glGetShaderiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSHADERIVPROC>(loader.Invoke("glGetShaderiv"));
            _glGetShaderInfoLog = Marshal.GetDelegateForFunctionPointer<PFNGLGETSHADERINFOLOGPROC>(loader.Invoke("glGetShaderInfoLog"));
            _glGetShaderSource = Marshal.GetDelegateForFunctionPointer<PFNGLGETSHADERSOURCEPROC>(loader.Invoke("glGetShaderSource"));
            _glGetUniformLocation = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMLOCATIONPROC>(loader.Invoke("glGetUniformLocation"));
            _glGetUniformfv = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMFVPROC>(loader.Invoke("glGetUniformfv"));
            _glGetUniformiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMIVPROC>(loader.Invoke("glGetUniformiv"));
            _glGetVertexAttribdv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBDVPROC>(loader.Invoke("glGetVertexAttribdv"));
            _glGetVertexAttribfv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBFVPROC>(loader.Invoke("glGetVertexAttribfv"));
            _glGetVertexAttribiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBIVPROC>(loader.Invoke("glGetVertexAttribiv"));
            _glGetVertexAttribPointerv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBPOINTERVPROC>(loader.Invoke("glGetVertexAttribPointerv"));
            _glIsProgram = Marshal.GetDelegateForFunctionPointer<PFNGLISPROGRAMPROC>(loader.Invoke("glIsProgram"));
            _glIsShader = Marshal.GetDelegateForFunctionPointer<PFNGLISSHADERPROC>(loader.Invoke("glIsShader"));
            _glLinkProgram = Marshal.GetDelegateForFunctionPointer<PFNGLLINKPROGRAMPROC>(loader.Invoke("glLinkProgram"));
            _glShaderSource = Marshal.GetDelegateForFunctionPointer<PFNGLSHADERSOURCEPROC>(loader.Invoke("glShaderSource"));
            _glUseProgram = Marshal.GetDelegateForFunctionPointer<PFNGLUSEPROGRAMPROC>(loader.Invoke("glUseProgram"));
            _glUniform1f = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1FPROC>(loader.Invoke("glUniform1f"));
            _glUniform2f = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2FPROC>(loader.Invoke("glUniform2f"));
            _glUniform3f = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3FPROC>(loader.Invoke("glUniform3f"));
            _glUniform4f = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4FPROC>(loader.Invoke("glUniform4f"));
            _glUniform1i = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1IPROC>(loader.Invoke("glUniform1i"));
            _glUniform2i = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2IPROC>(loader.Invoke("glUniform2i"));
            _glUniform3i = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3IPROC>(loader.Invoke("glUniform3i"));
            _glUniform4i = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4IPROC>(loader.Invoke("glUniform4i"));
            _glUniform1fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1FVPROC>(loader.Invoke("glUniform1fv"));
            _glUniform2fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2FVPROC>(loader.Invoke("glUniform2fv"));
            _glUniform3fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3FVPROC>(loader.Invoke("glUniform3fv"));
            _glUniform4fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4FVPROC>(loader.Invoke("glUniform4fv"));
            _glUniform1iv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1IVPROC>(loader.Invoke("glUniform1iv"));
            _glUniform2iv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2IVPROC>(loader.Invoke("glUniform2iv"));
            _glUniform3iv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3IVPROC>(loader.Invoke("glUniform3iv"));
            _glUniform4iv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4IVPROC>(loader.Invoke("glUniform4iv"));
            _glUniformMatrix2fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX2FVPROC>(loader.Invoke("glUniformMatrix2fv"));
            _glUniformMatrix3fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX3FVPROC>(loader.Invoke("glUniformMatrix3fv"));
            _glUniformMatrix4fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX4FVPROC>(loader.Invoke("glUniformMatrix4fv"));
            _glValidateProgram = Marshal.GetDelegateForFunctionPointer<PFNGLVALIDATEPROGRAMPROC>(loader.Invoke("glValidateProgram"));
            _glVertexAttrib1d = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1DPROC>(loader.Invoke("glVertexAttrib1d"));
            _glVertexAttrib1dv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1DVPROC>(loader.Invoke("glVertexAttrib1dv"));
            _glVertexAttrib1f = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1FPROC>(loader.Invoke("glVertexAttrib1f"));
            _glVertexAttrib1fv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1FVPROC>(loader.Invoke("glVertexAttrib1fv"));
            _glVertexAttrib1s = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1SPROC>(loader.Invoke("glVertexAttrib1s"));
            _glVertexAttrib1sv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB1SVPROC>(loader.Invoke("glVertexAttrib1sv"));
            _glVertexAttrib2d = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2DPROC>(loader.Invoke("glVertexAttrib2d"));
            _glVertexAttrib2dv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2DVPROC>(loader.Invoke("glVertexAttrib2dv"));
            _glVertexAttrib2f = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2FPROC>(loader.Invoke("glVertexAttrib2f"));
            _glVertexAttrib2fv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2FVPROC>(loader.Invoke("glVertexAttrib2fv"));
            _glVertexAttrib2s = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2SPROC>(loader.Invoke("glVertexAttrib2s"));
            _glVertexAttrib2sv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB2SVPROC>(loader.Invoke("glVertexAttrib2sv"));
            _glVertexAttrib3d = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3DPROC>(loader.Invoke("glVertexAttrib3d"));
            _glVertexAttrib3dv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3DVPROC>(loader.Invoke("glVertexAttrib3dv"));
            _glVertexAttrib3f = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3FPROC>(loader.Invoke("glVertexAttrib3f"));
            _glVertexAttrib3fv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3FVPROC>(loader.Invoke("glVertexAttrib3fv"));
            _glVertexAttrib3s = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3SPROC>(loader.Invoke("glVertexAttrib3s"));
            _glVertexAttrib3sv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB3SVPROC>(loader.Invoke("glVertexAttrib3sv"));
            _glVertexAttrib4Nbv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NBVPROC>(loader.Invoke("glVertexAttrib4Nbv"));
            _glVertexAttrib4Niv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NIVPROC>(loader.Invoke("glVertexAttrib4Niv"));
            _glVertexAttrib4Nsv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NSVPROC>(loader.Invoke("glVertexAttrib4Nsv"));
            _glVertexAttrib4Nub = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NUBPROC>(loader.Invoke("glVertexAttrib4Nub"));
            _glVertexAttrib4Nubv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NUBVPROC>(loader.Invoke("glVertexAttrib4Nubv"));
            _glVertexAttrib4Nuiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NUIVPROC>(loader.Invoke("glVertexAttrib4Nuiv"));
            _glVertexAttrib4Nusv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4NUSVPROC>(loader.Invoke("glVertexAttrib4Nusv"));
            _glVertexAttrib4bv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4BVPROC>(loader.Invoke("glVertexAttrib4bv"));
            _glVertexAttrib4d = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4DPROC>(loader.Invoke("glVertexAttrib4d"));
            _glVertexAttrib4dv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4DVPROC>(loader.Invoke("glVertexAttrib4dv"));
            _glVertexAttrib4f = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4FPROC>(loader.Invoke("glVertexAttrib4f"));
            _glVertexAttrib4fv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4FVPROC>(loader.Invoke("glVertexAttrib4fv"));
            _glVertexAttrib4iv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4IVPROC>(loader.Invoke("glVertexAttrib4iv"));
            _glVertexAttrib4s = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4SPROC>(loader.Invoke("glVertexAttrib4s"));
            _glVertexAttrib4sv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4SVPROC>(loader.Invoke("glVertexAttrib4sv"));
            _glVertexAttrib4ubv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4UBVPROC>(loader.Invoke("glVertexAttrib4ubv"));
            _glVertexAttrib4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4UIVPROC>(loader.Invoke("glVertexAttrib4uiv"));
            _glVertexAttrib4usv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIB4USVPROC>(loader.Invoke("glVertexAttrib4usv"));
            _glVertexAttribPointer = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBPOINTERPROC>(loader.Invoke("glVertexAttribPointer"));
            _glUniformMatrix2x3fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX2X3FVPROC>(loader.Invoke("glUniformMatrix2x3fv"));
            _glUniformMatrix3x2fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX3X2FVPROC>(loader.Invoke("glUniformMatrix3x2fv"));
            _glUniformMatrix2x4fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX2X4FVPROC>(loader.Invoke("glUniformMatrix2x4fv"));
            _glUniformMatrix4x2fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX4X2FVPROC>(loader.Invoke("glUniformMatrix4x2fv"));
            _glUniformMatrix3x4fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX3X4FVPROC>(loader.Invoke("glUniformMatrix3x4fv"));
            _glUniformMatrix4x3fv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMMATRIX4X3FVPROC>(loader.Invoke("glUniformMatrix4x3fv"));
            _glColorMaski = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORMASKIPROC>(loader.Invoke("glColorMaski"));
            _glGetBooleani_v = Marshal.GetDelegateForFunctionPointer<PFNGLGETBOOLEANI_VPROC>(loader.Invoke("glGetBooleani_v"));
            _glGetIntegeri_v = Marshal.GetDelegateForFunctionPointer<PFNGLGETINTEGERI_VPROC>(loader.Invoke("glGetIntegeri_v"));
            _glEnablei = Marshal.GetDelegateForFunctionPointer<PFNGLENABLEIPROC>(loader.Invoke("glEnablei"));
            _glDisablei = Marshal.GetDelegateForFunctionPointer<PFNGLDISABLEIPROC>(loader.Invoke("glDisablei"));
            _glIsEnabledi = Marshal.GetDelegateForFunctionPointer<PFNGLISENABLEDIPROC>(loader.Invoke("glIsEnabledi"));
            _glBeginTransformFeedback = Marshal.GetDelegateForFunctionPointer<PFNGLBEGINTRANSFORMFEEDBACKPROC>(loader.Invoke("glBeginTransformFeedback"));
            _glEndTransformFeedback = Marshal.GetDelegateForFunctionPointer<PFNGLENDTRANSFORMFEEDBACKPROC>(loader.Invoke("glEndTransformFeedback"));
            _glBindBufferRange = Marshal.GetDelegateForFunctionPointer<PFNGLBINDBUFFERRANGEPROC>(loader.Invoke("glBindBufferRange"));
            _glBindBufferBase = Marshal.GetDelegateForFunctionPointer<PFNGLBINDBUFFERBASEPROC>(loader.Invoke("glBindBufferBase"));
            _glTransformFeedbackVaryings = Marshal.GetDelegateForFunctionPointer<PFNGLTRANSFORMFEEDBACKVARYINGSPROC>(loader.Invoke("glTransformFeedbackVaryings"));
            _glGetTransformFeedbackVarying = Marshal.GetDelegateForFunctionPointer<PFNGLGETTRANSFORMFEEDBACKVARYINGPROC>(loader.Invoke("glGetTransformFeedbackVarying"));
            _glClampColor = Marshal.GetDelegateForFunctionPointer<PFNGLCLAMPCOLORPROC>(loader.Invoke("glClampColor"));
            _glBeginConditionalRender = Marshal.GetDelegateForFunctionPointer<PFNGLBEGINCONDITIONALRENDERPROC>(loader.Invoke("glBeginConditionalRender"));
            _glEndConditionalRender = Marshal.GetDelegateForFunctionPointer<PFNGLENDCONDITIONALRENDERPROC>(loader.Invoke("glEndConditionalRender"));
            _glVertexAttribIPointer = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBIPOINTERPROC>(loader.Invoke("glVertexAttribIPointer"));
            _glGetVertexAttribIiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBIIVPROC>(loader.Invoke("glGetVertexAttribIiv"));
            _glGetVertexAttribIuiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETVERTEXATTRIBIUIVPROC>(loader.Invoke("glGetVertexAttribIuiv"));
            _glVertexAttribI1i = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI1IPROC>(loader.Invoke("glVertexAttribI1i"));
            _glVertexAttribI2i = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI2IPROC>(loader.Invoke("glVertexAttribI2i"));
            _glVertexAttribI3i = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI3IPROC>(loader.Invoke("glVertexAttribI3i"));
            _glVertexAttribI4i = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4IPROC>(loader.Invoke("glVertexAttribI4i"));
            _glVertexAttribI1ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI1UIPROC>(loader.Invoke("glVertexAttribI1ui"));
            _glVertexAttribI2ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI2UIPROC>(loader.Invoke("glVertexAttribI2ui"));
            _glVertexAttribI3ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI3UIPROC>(loader.Invoke("glVertexAttribI3ui"));
            _glVertexAttribI4ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4UIPROC>(loader.Invoke("glVertexAttribI4ui"));
            _glVertexAttribI1iv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI1IVPROC>(loader.Invoke("glVertexAttribI1iv"));
            _glVertexAttribI2iv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI2IVPROC>(loader.Invoke("glVertexAttribI2iv"));
            _glVertexAttribI3iv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI3IVPROC>(loader.Invoke("glVertexAttribI3iv"));
            _glVertexAttribI4iv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4IVPROC>(loader.Invoke("glVertexAttribI4iv"));
            _glVertexAttribI1uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI1UIVPROC>(loader.Invoke("glVertexAttribI1uiv"));
            _glVertexAttribI2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI2UIVPROC>(loader.Invoke("glVertexAttribI2uiv"));
            _glVertexAttribI3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI3UIVPROC>(loader.Invoke("glVertexAttribI3uiv"));
            _glVertexAttribI4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4UIVPROC>(loader.Invoke("glVertexAttribI4uiv"));
            _glVertexAttribI4bv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4BVPROC>(loader.Invoke("glVertexAttribI4bv"));
            _glVertexAttribI4sv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4SVPROC>(loader.Invoke("glVertexAttribI4sv"));
            _glVertexAttribI4ubv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4UBVPROC>(loader.Invoke("glVertexAttribI4ubv"));
            _glVertexAttribI4usv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBI4USVPROC>(loader.Invoke("glVertexAttribI4usv"));
            _glGetUniformuiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMUIVPROC>(loader.Invoke("glGetUniformuiv"));
            _glBindFragDataLocation = Marshal.GetDelegateForFunctionPointer<PFNGLBINDFRAGDATALOCATIONPROC>(loader.Invoke("glBindFragDataLocation"));
            _glGetFragDataLocation = Marshal.GetDelegateForFunctionPointer<PFNGLGETFRAGDATALOCATIONPROC>(loader.Invoke("glGetFragDataLocation"));
            _glUniform1ui = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1UIPROC>(loader.Invoke("glUniform1ui"));
            _glUniform2ui = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2UIPROC>(loader.Invoke("glUniform2ui"));
            _glUniform3ui = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3UIPROC>(loader.Invoke("glUniform3ui"));
            _glUniform4ui = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4UIPROC>(loader.Invoke("glUniform4ui"));
            _glUniform1uiv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM1UIVPROC>(loader.Invoke("glUniform1uiv"));
            _glUniform2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM2UIVPROC>(loader.Invoke("glUniform2uiv"));
            _glUniform3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM3UIVPROC>(loader.Invoke("glUniform3uiv"));
            _glUniform4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORM4UIVPROC>(loader.Invoke("glUniform4uiv"));
            _glTexParameterIiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERIIVPROC>(loader.Invoke("glTexParameterIiv"));
            _glTexParameterIuiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXPARAMETERIUIVPROC>(loader.Invoke("glTexParameterIuiv"));
            _glGetTexParameterIiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXPARAMETERIIVPROC>(loader.Invoke("glGetTexParameterIiv"));
            _glGetTexParameterIuiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETTEXPARAMETERIUIVPROC>(loader.Invoke("glGetTexParameterIuiv"));
            _glClearBufferiv = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARBUFFERIVPROC>(loader.Invoke("glClearBufferiv"));
            _glClearBufferuiv = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARBUFFERUIVPROC>(loader.Invoke("glClearBufferuiv"));
            _glClearBufferfv = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARBUFFERFVPROC>(loader.Invoke("glClearBufferfv"));
            _glClearBufferfi = Marshal.GetDelegateForFunctionPointer<PFNGLCLEARBUFFERFIPROC>(loader.Invoke("glClearBufferfi"));
            _glGetStringi = Marshal.GetDelegateForFunctionPointer<PFNGLGETSTRINGIPROC>(loader.Invoke("glGetStringi"));
            _glIsRenderbuffer = Marshal.GetDelegateForFunctionPointer<PFNGLISRENDERBUFFERPROC>(loader.Invoke("glIsRenderbuffer"));
            _glBindRenderbuffer = Marshal.GetDelegateForFunctionPointer<PFNGLBINDRENDERBUFFERPROC>(loader.Invoke("glBindRenderbuffer"));
            _glDeleteRenderbuffers = Marshal.GetDelegateForFunctionPointer<PFNGLDELETERENDERBUFFERSPROC>(loader.Invoke("glDeleteRenderbuffers"));
            _glGenRenderbuffers = Marshal.GetDelegateForFunctionPointer<PFNGLGENRENDERBUFFERSPROC>(loader.Invoke("glGenRenderbuffers"));
            _glRenderbufferStorage = Marshal.GetDelegateForFunctionPointer<PFNGLRENDERBUFFERSTORAGEPROC>(loader.Invoke("glRenderbufferStorage"));
            _glGetRenderbufferParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETRENDERBUFFERPARAMETERIVPROC>(loader.Invoke("glGetRenderbufferParameteriv"));
            _glIsFramebuffer = Marshal.GetDelegateForFunctionPointer<PFNGLISFRAMEBUFFERPROC>(loader.Invoke("glIsFramebuffer"));
            _glBindFramebuffer = Marshal.GetDelegateForFunctionPointer<PFNGLBINDFRAMEBUFFERPROC>(loader.Invoke("glBindFramebuffer"));
            _glDeleteFramebuffers = Marshal.GetDelegateForFunctionPointer<PFNGLDELETEFRAMEBUFFERSPROC>(loader.Invoke("glDeleteFramebuffers"));
            _glGenFramebuffers = Marshal.GetDelegateForFunctionPointer<PFNGLGENFRAMEBUFFERSPROC>(loader.Invoke("glGenFramebuffers"));
            _glCheckFramebufferStatus = Marshal.GetDelegateForFunctionPointer<PFNGLCHECKFRAMEBUFFERSTATUSPROC>(loader.Invoke("glCheckFramebufferStatus"));
            _glFramebufferTexture1D = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERTEXTURE1DPROC>(loader.Invoke("glFramebufferTexture1D"));
            _glFramebufferTexture2D = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERTEXTURE2DPROC>(loader.Invoke("glFramebufferTexture2D"));
            _glFramebufferTexture3D = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERTEXTURE3DPROC>(loader.Invoke("glFramebufferTexture3D"));
            _glFramebufferRenderbuffer = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERRENDERBUFFERPROC>(loader.Invoke("glFramebufferRenderbuffer"));
            _glGetFramebufferAttachmentParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETFRAMEBUFFERATTACHMENTPARAMETERIVPROC>(loader.Invoke("glGetFramebufferAttachmentParameteriv"));
            _glGenerateMipmap = Marshal.GetDelegateForFunctionPointer<PFNGLGENERATEMIPMAPPROC>(loader.Invoke("glGenerateMipmap"));
            _glBlitFramebuffer = Marshal.GetDelegateForFunctionPointer<PFNGLBLITFRAMEBUFFERPROC>(loader.Invoke("glBlitFramebuffer"));
            _glRenderbufferStorageMultisample = Marshal.GetDelegateForFunctionPointer<PFNGLRENDERBUFFERSTORAGEMULTISAMPLEPROC>(loader.Invoke("glRenderbufferStorageMultisample"));
            _glFramebufferTextureLayer = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERTEXTURELAYERPROC>(loader.Invoke("glFramebufferTextureLayer"));
            _glMapBufferRange = Marshal.GetDelegateForFunctionPointer<PFNGLMAPBUFFERRANGEPROC>(loader.Invoke("glMapBufferRange"));
            _glFlushMappedBufferRange = Marshal.GetDelegateForFunctionPointer<PFNGLFLUSHMAPPEDBUFFERRANGEPROC>(loader.Invoke("glFlushMappedBufferRange"));
            _glBindVertexArray = Marshal.GetDelegateForFunctionPointer<PFNGLBINDVERTEXARRAYPROC>(loader.Invoke("glBindVertexArray"));
            _glDeleteVertexArrays = Marshal.GetDelegateForFunctionPointer<PFNGLDELETEVERTEXARRAYSPROC>(loader.Invoke("glDeleteVertexArrays"));
            _glGenVertexArrays = Marshal.GetDelegateForFunctionPointer<PFNGLGENVERTEXARRAYSPROC>(loader.Invoke("glGenVertexArrays"));
            _glIsVertexArray = Marshal.GetDelegateForFunctionPointer<PFNGLISVERTEXARRAYPROC>(loader.Invoke("glIsVertexArray"));
            _glDrawArraysInstanced = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWARRAYSINSTANCEDPROC>(loader.Invoke("glDrawArraysInstanced"));
            _glDrawElementsInstanced = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWELEMENTSINSTANCEDPROC>(loader.Invoke("glDrawElementsInstanced"));
            _glTexBuffer = Marshal.GetDelegateForFunctionPointer<PFNGLTEXBUFFERPROC>(loader.Invoke("glTexBuffer"));
            _glPrimitiveRestartIndex = Marshal.GetDelegateForFunctionPointer<PFNGLPRIMITIVERESTARTINDEXPROC>(loader.Invoke("glPrimitiveRestartIndex"));
            _glCopyBufferSubData = Marshal.GetDelegateForFunctionPointer<PFNGLCOPYBUFFERSUBDATAPROC>(loader.Invoke("glCopyBufferSubData"));
            _glGetUniformIndices = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMINDICESPROC>(loader.Invoke("glGetUniformIndices"));
            _glGetActiveUniformsiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEUNIFORMSIVPROC>(loader.Invoke("glGetActiveUniformsiv"));
            _glGetActiveUniformName = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEUNIFORMNAMEPROC>(loader.Invoke("glGetActiveUniformName"));
            _glGetUniformBlockIndex = Marshal.GetDelegateForFunctionPointer<PFNGLGETUNIFORMBLOCKINDEXPROC>(loader.Invoke("glGetUniformBlockIndex"));
            _glGetActiveUniformBlockiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEUNIFORMBLOCKIVPROC>(loader.Invoke("glGetActiveUniformBlockiv"));
            _glGetActiveUniformBlockName = Marshal.GetDelegateForFunctionPointer<PFNGLGETACTIVEUNIFORMBLOCKNAMEPROC>(loader.Invoke("glGetActiveUniformBlockName"));
            _glUniformBlockBinding = Marshal.GetDelegateForFunctionPointer<PFNGLUNIFORMBLOCKBINDINGPROC>(loader.Invoke("glUniformBlockBinding"));
            _glBindBufferRange = Marshal.GetDelegateForFunctionPointer<PFNGLBINDBUFFERRANGEPROC>(loader.Invoke("glBindBufferRange"));
            _glBindBufferBase = Marshal.GetDelegateForFunctionPointer<PFNGLBINDBUFFERBASEPROC>(loader.Invoke("glBindBufferBase"));
            _glGetIntegeri_v = Marshal.GetDelegateForFunctionPointer<PFNGLGETINTEGERI_VPROC>(loader.Invoke("glGetIntegeri_v"));
            _glDrawElementsBaseVertex = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWELEMENTSBASEVERTEXPROC>(loader.Invoke("glDrawElementsBaseVertex"));
            _glDrawRangeElementsBaseVertex = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWRANGEELEMENTSBASEVERTEXPROC>(loader.Invoke("glDrawRangeElementsBaseVertex"));
            _glDrawElementsInstancedBaseVertex = Marshal.GetDelegateForFunctionPointer<PFNGLDRAWELEMENTSINSTANCEDBASEVERTEXPROC>(loader.Invoke("glDrawElementsInstancedBaseVertex"));
            _glMultiDrawElementsBaseVertex = Marshal.GetDelegateForFunctionPointer<PFNGLMULTIDRAWELEMENTSBASEVERTEXPROC>(loader.Invoke("glMultiDrawElementsBaseVertex"));
            _glProvokingVertex = Marshal.GetDelegateForFunctionPointer<PFNGLPROVOKINGVERTEXPROC>(loader.Invoke("glProvokingVertex"));
            _glFenceSync = Marshal.GetDelegateForFunctionPointer<PFNGLFENCESYNCPROC>(loader.Invoke("glFenceSync"));
            _glIsSync = Marshal.GetDelegateForFunctionPointer<PFNGLISSYNCPROC>(loader.Invoke("glIsSync"));
            _glDeleteSync = Marshal.GetDelegateForFunctionPointer<PFNGLDELETESYNCPROC>(loader.Invoke("glDeleteSync"));
            _glClientWaitSync = Marshal.GetDelegateForFunctionPointer<PFNGLCLIENTWAITSYNCPROC>(loader.Invoke("glClientWaitSync"));
            _glWaitSync = Marshal.GetDelegateForFunctionPointer<PFNGLWAITSYNCPROC>(loader.Invoke("glWaitSync"));
            _glGetInteger64v = Marshal.GetDelegateForFunctionPointer<PFNGLGETINTEGER64VPROC>(loader.Invoke("glGetInteger64v"));
            _glGetSynciv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSYNCIVPROC>(loader.Invoke("glGetSynciv"));
            _glGetInteger64i_v = Marshal.GetDelegateForFunctionPointer<PFNGLGETINTEGER64I_VPROC>(loader.Invoke("glGetInteger64i_v"));
            _glGetBufferParameteri64v = Marshal.GetDelegateForFunctionPointer<PFNGLGETBUFFERPARAMETERI64VPROC>(loader.Invoke("glGetBufferParameteri64v"));
            _glFramebufferTexture = Marshal.GetDelegateForFunctionPointer<PFNGLFRAMEBUFFERTEXTUREPROC>(loader.Invoke("glFramebufferTexture"));
            _glTexImage2DMultisample = Marshal.GetDelegateForFunctionPointer<PFNGLTEXIMAGE2DMULTISAMPLEPROC>(loader.Invoke("glTexImage2DMultisample"));
            _glTexImage3DMultisample = Marshal.GetDelegateForFunctionPointer<PFNGLTEXIMAGE3DMULTISAMPLEPROC>(loader.Invoke("glTexImage3DMultisample"));
            _glGetMultisamplefv = Marshal.GetDelegateForFunctionPointer<PFNGLGETMULTISAMPLEFVPROC>(loader.Invoke("glGetMultisamplefv"));
            _glSampleMaski = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLEMASKIPROC>(loader.Invoke("glSampleMaski"));
            _glBindFragDataLocationIndexed = Marshal.GetDelegateForFunctionPointer<PFNGLBINDFRAGDATALOCATIONINDEXEDPROC>(loader.Invoke("glBindFragDataLocationIndexed"));
            _glGetFragDataIndex = Marshal.GetDelegateForFunctionPointer<PFNGLGETFRAGDATAINDEXPROC>(loader.Invoke("glGetFragDataIndex"));
            _glGenSamplers = Marshal.GetDelegateForFunctionPointer<PFNGLGENSAMPLERSPROC>(loader.Invoke("glGenSamplers"));
            _glDeleteSamplers = Marshal.GetDelegateForFunctionPointer<PFNGLDELETESAMPLERSPROC>(loader.Invoke("glDeleteSamplers"));
            _glIsSampler = Marshal.GetDelegateForFunctionPointer<PFNGLISSAMPLERPROC>(loader.Invoke("glIsSampler"));
            _glBindSampler = Marshal.GetDelegateForFunctionPointer<PFNGLBINDSAMPLERPROC>(loader.Invoke("glBindSampler"));
            _glSamplerParameteri = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERIPROC>(loader.Invoke("glSamplerParameteri"));
            _glSamplerParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERIVPROC>(loader.Invoke("glSamplerParameteriv"));
            _glSamplerParameterf = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERFPROC>(loader.Invoke("glSamplerParameterf"));
            _glSamplerParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERFVPROC>(loader.Invoke("glSamplerParameterfv"));
            _glSamplerParameterIiv = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERIIVPROC>(loader.Invoke("glSamplerParameterIiv"));
            _glSamplerParameterIuiv = Marshal.GetDelegateForFunctionPointer<PFNGLSAMPLERPARAMETERIUIVPROC>(loader.Invoke("glSamplerParameterIuiv"));
            _glGetSamplerParameteriv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSAMPLERPARAMETERIVPROC>(loader.Invoke("glGetSamplerParameteriv"));
            _glGetSamplerParameterIiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSAMPLERPARAMETERIIVPROC>(loader.Invoke("glGetSamplerParameterIiv"));
            _glGetSamplerParameterfv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSAMPLERPARAMETERFVPROC>(loader.Invoke("glGetSamplerParameterfv"));
            _glGetSamplerParameterIuiv = Marshal.GetDelegateForFunctionPointer<PFNGLGETSAMPLERPARAMETERIUIVPROC>(loader.Invoke("glGetSamplerParameterIuiv"));
            _glQueryCounter = Marshal.GetDelegateForFunctionPointer<PFNGLQUERYCOUNTERPROC>(loader.Invoke("glQueryCounter"));
            _glGetQueryObjecti64v = Marshal.GetDelegateForFunctionPointer<PFNGLGETQUERYOBJECTI64VPROC>(loader.Invoke("glGetQueryObjecti64v"));
            _glGetQueryObjectui64v = Marshal.GetDelegateForFunctionPointer<PFNGLGETQUERYOBJECTUI64VPROC>(loader.Invoke("glGetQueryObjectui64v"));
            _glVertexAttribDivisor = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBDIVISORPROC>(loader.Invoke("glVertexAttribDivisor"));
            _glVertexAttribP1ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP1UIPROC>(loader.Invoke("glVertexAttribP1ui"));
            _glVertexAttribP1uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP1UIVPROC>(loader.Invoke("glVertexAttribP1uiv"));
            _glVertexAttribP2ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP2UIPROC>(loader.Invoke("glVertexAttribP2ui"));
            _glVertexAttribP2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP2UIVPROC>(loader.Invoke("glVertexAttribP2uiv"));
            _glVertexAttribP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP3UIPROC>(loader.Invoke("glVertexAttribP3ui"));
            _glVertexAttribP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP3UIVPROC>(loader.Invoke("glVertexAttribP3uiv"));
            _glVertexAttribP4ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP4UIPROC>(loader.Invoke("glVertexAttribP4ui"));
            _glVertexAttribP4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXATTRIBP4UIVPROC>(loader.Invoke("glVertexAttribP4uiv"));
            _glVertexP2ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP2UIPROC>(loader.Invoke("glVertexP2ui"));
            _glVertexP2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP2UIVPROC>(loader.Invoke("glVertexP2uiv"));
            _glVertexP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP3UIPROC>(loader.Invoke("glVertexP3ui"));
            _glVertexP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP3UIVPROC>(loader.Invoke("glVertexP3uiv"));
            _glVertexP4ui = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP4UIPROC>(loader.Invoke("glVertexP4ui"));
            _glVertexP4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLVERTEXP4UIVPROC>(loader.Invoke("glVertexP4uiv"));
            _glTexCoordP1ui = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP1UIPROC>(loader.Invoke("glTexCoordP1ui"));
            _glTexCoordP1uiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP1UIVPROC>(loader.Invoke("glTexCoordP1uiv"));
            _glTexCoordP2ui = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP2UIPROC>(loader.Invoke("glTexCoordP2ui"));
            _glTexCoordP2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP2UIVPROC>(loader.Invoke("glTexCoordP2uiv"));
            _glTexCoordP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP3UIPROC>(loader.Invoke("glTexCoordP3ui"));
            _glTexCoordP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP3UIVPROC>(loader.Invoke("glTexCoordP3uiv"));
            _glTexCoordP4ui = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP4UIPROC>(loader.Invoke("glTexCoordP4ui"));
            _glTexCoordP4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLTEXCOORDP4UIVPROC>(loader.Invoke("glTexCoordP4uiv"));
            _glMultiTexCoordP1ui = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP1UIPROC>(loader.Invoke("glMultiTexCoordP1ui"));
            _glMultiTexCoordP1uiv = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP1UIVPROC>(loader.Invoke("glMultiTexCoordP1uiv"));
            _glMultiTexCoordP2ui = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP2UIPROC>(loader.Invoke("glMultiTexCoordP2ui"));
            _glMultiTexCoordP2uiv = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP2UIVPROC>(loader.Invoke("glMultiTexCoordP2uiv"));
            _glMultiTexCoordP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP3UIPROC>(loader.Invoke("glMultiTexCoordP3ui"));
            _glMultiTexCoordP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP3UIVPROC>(loader.Invoke("glMultiTexCoordP3uiv"));
            _glMultiTexCoordP4ui = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP4UIPROC>(loader.Invoke("glMultiTexCoordP4ui"));
            _glMultiTexCoordP4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLMULTITEXCOORDP4UIVPROC>(loader.Invoke("glMultiTexCoordP4uiv"));
            _glNormalP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLNORMALP3UIPROC>(loader.Invoke("glNormalP3ui"));
            _glNormalP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLNORMALP3UIVPROC>(loader.Invoke("glNormalP3uiv"));
            _glColorP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORP3UIPROC>(loader.Invoke("glColorP3ui"));
            _glColorP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORP3UIVPROC>(loader.Invoke("glColorP3uiv"));
            _glColorP4ui = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORP4UIPROC>(loader.Invoke("glColorP4ui"));
            _glColorP4uiv = Marshal.GetDelegateForFunctionPointer<PFNGLCOLORP4UIVPROC>(loader.Invoke("glColorP4uiv"));
            _glSecondaryColorP3ui = Marshal.GetDelegateForFunctionPointer<PFNGLSECONDARYCOLORP3UIPROC>(loader.Invoke("glSecondaryColorP3ui"));
            _glSecondaryColorP3uiv = Marshal.GetDelegateForFunctionPointer<PFNGLSECONDARYCOLORP3UIVPROC>(loader.Invoke("glSecondaryColorP3uiv"));
        }

        #endregion Private logic

        #region External functions

        public static void glBegin(uint mode) => _glBegin((int)mode);

        public static void glEnd() => _glEnd();

        public static void glVertex3f(float v1, float v2, float v3) => _glVertex3f(v1, v2, v3);

        public static void glColor3f(float v1, float v2, float v3) => _glColor3f(v1, v2, v3);

        public static void glCullFace(int mode) => _glCullFace(mode);

        public static void glMultMatrixf(float[] matrix) => _glMultMatrixf(matrix);

        public static void glMatrixMode(uint projection) => _glMatrixMode(projection);

        public static void glLoadIdentity() => _glLoadIdentity();

        public static void glOrtho(float left, float rigth, float bottom, float top, float nearVal, float farVal) => _glOrtho(left, rigth, bottom, top, nearVal, farVal);

        public static void glFrustum(float left, float rigth, float bottom, float top, float nearVal, float farVal) => _glFrustum(left, rigth, bottom, top, nearVal, farVal);

        public static void glFrontFace(int mode) => _glFrontFace(mode);

        public static void glHint(int target, int mode) => _glHint(target, mode);

        public static void glLineWidth(float width) => _glLineWidth(width);

        public static void glPointSize(float size) => _glPointSize(size);

        public static void glPolygonMode(int face, int mode) => _glPolygonMode(face, mode);

        public static void glScissor(int x, int y, int width, int height) => _glScissor(x, y, width, height);

        public static void glClearColor(float red, float green, float blue, float alpha) => _glClearColor(red, green, blue, alpha);

        public static void glClear(uint mask) => _glClear(mask);

        public static void glFinish() => _glFinish();

        public static void glFlush() => _glFlush();

        public static void glEnable(int cap) => _glEnable(cap);

        public static void glDisable(int cap) => _glDisable(cap);

        public static void glClearStencil(int index) => _glClearStencil(index);

        public static void glClearDepth(float depth) => _glClearDepth(depth);

        public static void glStencilMask(uint mask) => _glStencilMask(mask);

        public static void glColorMask(bool red, bool green, bool blue, bool alpha) => _glColorMask(red, green, blue, alpha);

        public static void glColorMaski(uint index, bool red, bool green, bool blue, bool alpha) => _glColorMaski(index, red, green, blue, alpha);

        public static void glDepthMask(bool enabled) => _glDepthMask(enabled);

        public static void glBlendColor(float red, float green, float blue, float alpha) => _glBlendColor(red, green, blue, alpha);

        public static void glBlendFunc(int srcFactor, int dstFactor) => _glBlendFunc(srcFactor, dstFactor);

        public static void glBlendEquation(int mode) => _glBlendEquation(mode);

        public static void glViewport(int x, int y, int width, int height) => _glViewport(x, y, width, height);

        public static bool glIsEnabled(int cap) => _glIsEnabled(cap);

        public static void glDrawArrays(int mode, int first, int count) => _glDrawArrays(mode, first, count);

        public static void glDrawBuffer(int buffer) => _glDrawBuffer(buffer);

        public static void glReadBuffer(int buffer) => _glReadBuffer(buffer);

        public static void glLogicOp(int opcode) => _glLogicOp(opcode);

        public static void glStencilFunc(int func, int reference, uint mask) => _glStencilFunc(func, reference, mask);

        public static void glStencilOp(int fail, int zfail, int zpass) => _glStencilOp(fail, zfail, zpass);

        public static void glDepthFunc(int func) => _glDepthFunc(func);

        public static void glBeginConditionalRender(uint id, int mode) => _glBeginConditionalRender(id, mode);

        public static void glEndConditionalRender() => _glEndConditionalRender();

        public static void glClampColor(bool clamp) => _glClampColor(GlfwConstants.GL_CLAMP_READ_COLOR, clamp ? GlfwConstants.GL_TRUE : GlfwConstants.GL_FALSE);



        public static string glGetString(int name)
        {
            var buffer = new IntPtr(_glGetString(name));
            return PtrToStringUtf8(buffer);
        }



        public static string glGetStringi(int name, uint index)
        {
            var buffer = new IntPtr(_glGetStringi(name, index));
            return PtrToStringUtf8(buffer);
        }

        public static void glPixelStorei(int paramName, int param) => _glPixelStorei(paramName, param);

        public static void glPixelStoref(int paramName, float param) => _glPixelStoref(paramName, param);

        public static void glBufferData(int target, int size, IntPtr data, int usage) => _glBufferData(target, new IntPtr(size), data.ToPointer(), usage);

        public static void glBufferData(int target, int size, /*const*/ void* data, int usage) => _glBufferData(target, new IntPtr(size), data, usage);

        public static int GetError() => _glGetError();

        public static void glTexParameterf(int target, int paramName, float param) => _glTexParameterf(target, paramName, param);

        public static void glTexParameteri(int target, int paramName, int param) => _glTexParameteri(target, paramName, param);

        public static void glTexParameterfv(int target, int paramName, float* param) => _glTexParameterfv(target, paramName, param);

        public static void glTexParameteriv(int target, int paramName, int* param) => _glTexParameteriv(target, paramName, param);

        public static void glTexParameterfv(int target, int paramName, float[] param)
        {
            fixed (float* p = &param[0])
            {
                _glTexParameterfv(target, paramName, p);
            }
        }

        public static void glTexParameteriv(int target, int paramName, int[] param)
        {
            fixed (int* p = &param[0])
            {
                _glTexParameteriv(target, paramName, p);
            }
        }

        public static void glDepthRange(float near, float far) => _glDepthRange(near, far);

        public static void glDrawElements(int mode, int count, int type, /*const*/ void* indices) => _glDrawElements(mode, count, type, indices);

        public static void glDrawElements(int mode, int numOfIndices, byte[] indices)
        {
            fixed (void* pointer = &indices[0])
            {
                _glDrawElements(mode, indices.Length, GlfwConstants.GL_UNSIGNED_BYTE, pointer);
            }
        }

        public static void glDrawElements(int mode, ushort[] indices)
        {
            fixed (void* pointer = &indices[0])
            {
                _glDrawElements(mode, indices.Length, GlfwConstants.GL_UNSIGNED_SHORT, pointer);
            }
        }

        public static void glDrawElements(int mode, uint[] indices)
        {
            fixed (void* pointer = &indices[0])
            {
                _glDrawElements(mode, indices.Length, GlfwConstants.GL_UNSIGNED_INT, pointer);
            }
        }

        public static void glGetTexImage(int target, int level, int format, int type, void* pixels) => _glGetTexImage(target, level, format, type, pixels);

        public static void glGetTexImage(int target, int level, int format, int type, IntPtr pixels) => _glGetTexImage(target, level, format, type, pixels.ToPointer());

        public static void glReadPixels(int x, int y, int width, int height, int format, int type, void* pixels) => _glReadPixels(x, y, width, height, format, type, pixels);

        public static void glReadPixels(int x, int y, int width, int height, int format, int type, IntPtr pixels) => _glReadPixels(x, y, width, height, format, type, pixels.ToPointer());

        public static void glReadPixels(int x, int y, int width, int height, int format, int type, byte[] pixels)
        {
            var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            var ptr = handle.AddrOfPinnedObject();
            _glReadPixels(x, y, width, height, format, type, ptr.ToPointer());
            handle.Free();
        }

        public static void glDrawBuffers(int n, /*const*/ int* buffers) => _glDrawBuffers(n, buffers);

        public static void glDrawBuffers(int[] buffers)
        {
            fixed (int* buf = &buffers[0])
            {
                _glDrawBuffers(buffers.Length, buf);
            }
        }

        public static void glTexImage1D(int target, int level, int internalFormat, int width, int border, int format, int type, IntPtr pixels) => _glTexImage1D(target, level, internalFormat, width, border, format, type, pixels.ToPointer());

        public static void glTexImage1D(int target, int level, int internalFormat, int width, int border, int format, int type, /*const*/ void* pixels) => _glTexImage1D(target, level, internalFormat, width, border, format, type, pixels);

        public static void glTexImage2D(int target, int level, int internalFormat, int width, int height, int border, int format, int type, IntPtr pixels) => _glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixels.ToPointer());

        public static void glTexImage2D(int target, int level, int internalFormat, int width, int height, int border, int format, int type, /*const*/ void* pixels) => _glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);

        public static void glTexImage3D(int target, int level, int internalFormat, int width, int height, int depth, int border, int format, int type, IntPtr pixels) => _glTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, pixels.ToPointer());

        public static void glTexImage3D(int target, int level, int internalFormat, int width, int height, int depth, int border, int format, int type, /*const*/ void* pixels) => _glTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, pixels);

        public static void glBindTexture(int target, uint texture) => _glBindTexture(target, texture);

        public static void glActiveTexture(int texture) => _glActiveTexture(texture);

        public static void glDeleteTextures(int n, /*const*/ uint* textures) => _glDeleteTextures(n, textures);

        public static void glDeleteTextures(uint[] textures)
        {
            if (textures is null)
                return;
            fixed (uint* ids = &textures[0])
            {
                _glDeleteTextures(textures.Length, ids);
            }
        }

        public static void glDeleteTexture(uint texture) => _glDeleteTextures(1, &texture);

        public static bool glIsTexture(uint texture) => _glIsTexture(texture);

        public static void glGenTextures(int n, uint* textures) => _glGenTextures(n, textures);



        public static uint[] glGenTextures(int n)
        {
            var textures = new uint[n];
            fixed (uint* ids = &textures[0])
            {
                _glGenTextures(n, ids);
            }

            return textures;
        }

        public static uint glGenTexture()
        {
            uint texture;
            _glGenTextures(1, &texture);
            return texture;
        }



        public static uint glGenQuery()
        {
            uint id;
            _glGenQueries(1, &id);
            return id;
        }

        public static void glGenQueries(int n, uint* ids) => _glGenQueries(n, ids);



        public static uint[] glGenQueries(int n)
        {
            var queries = new uint[n];
            fixed (uint* ids = &queries[0])
            {
                _glGenQueries(n, ids);
            }

            return queries;
        }

        public static void glPolygonOffset(float factor, float units) => _glPolygonOffset(factor, units);

        public static void glProvokingVertex(int mode) => _glProvokingVertex(mode);

        public static void glGetCompressedTexImage(int target, int level, IntPtr pixels) => _glGetCompressedTexImage(target, level, pixels.ToPointer());

        public static void glGetCompressedTexImage(int target, int level, void* pixels) => _glGetCompressedTexImage(target, level, pixels);

        public static void glSampleCoverage(float value, bool invert) => _glSampleCoverage(value, invert);

        public static void glBeginQuery(int target, uint id) => _glBeginQuery(target, id);

        public static void glEndQuery(int target) => _glEndQuery(target);

        public static bool glIsQuery(uint id) => _glIsQuery(id);

        public static void glDeleteQueries(int n, /*const*/ uint* ids) => _glDeleteQueries(n, ids);

        public static void glDeleteQueries(uint[] ids)
        {
            fixed (uint* names = &ids[0])
            {
                _glDeleteQueries(ids.Length, names);
            }
        }

        public static void glDeleteQuery(uint id) => _glDeleteQueries(1, &id);

        public static void glBlendEquationSeparate(int modeRGB, int modeAlpha) => _glBlendEquationSeparate(modeRGB, modeAlpha);

        public static void glStencilFuncSeparate(int face, int func, int reference, uint mask) => _glStencilFuncSeparate(face, func, reference, mask);

        public static void glStencilOpSeparate(int face, int sfail, int dpfail, int dppass) => _glStencilOpSeparate(face, sfail, dpfail, dppass);

        public static void glStencilMaskSeparate(int face, uint mask) => _glStencilMaskSeparate(face, mask);

        public static void glWaitSync(IntPtr sync, uint flags, ulong timeout) => _glWaitSync(sync, flags, timeout);

        public static IntPtr glFenceSync(int condition, uint flags = 0) => _glFenceSync(condition, flags);

        public static void glDeleteSync(IntPtr sync) => _glDeleteSync(sync);

        public static bool glIsSync(IntPtr sync) => _glIsSync(sync);

        public static int glClientWaitSync(IntPtr sync, uint flags, ulong timeout) => _glClientWaitSync(sync, flags, timeout);

        public static void glGetBooleanv(int paramName, bool* data) => _glGetBooleanv(paramName, data);


        public static bool glGetBoolean(int paramName)
        {
            bool value;
            _glGetBooleanv(paramName, &value);
            return value;
        }



        public static bool[] glGetBooleanv(int paramName, int count)
        {
            var value = new bool[count];
            fixed (bool* v = &value[0])
            {
                _glGetBooleanv(paramName, v);
            }
            return value;
        }

        public static void glGetDoublev(int paramName, float* data) => _glGetDoublev(paramName, data);


        public static float glGetDouble(int paramName)
        {
            float value;
            _glGetDoublev(paramName, &value);
            return value;
        }



        public static float[] glGetDoublev(int paramName, int count)
        {
            var value = new float[count];
            fixed (float* v = &value[0])
            {
                _glGetDoublev(paramName, v);
            }
            return value;
        }


        public static void glGetFloatv(int paramName, float* data) => _glGetFloatv(paramName, data);


        public static float glGetFloat(int paramName)
        {
            float value;
            _glGetFloatv(paramName, &value);
            return value;
        }



        public static float[] glGetFloatv(int paramName, int count)
        {
            var value = new float[count];
            fixed (float* v = &value[0])
            {
                _glGetFloatv(paramName, v);
            }
            return value;
        }

        public static void glGetIntegerv(int paramName, int* data) => _glGetIntegerv(paramName, data);


        public static int glGetInteger(int paramName)
        {
            int value;
            _glGetIntegerv(paramName, &value);
            return value;
        }



        public static int[] glGetIntegerv(int paramName, int count)
        {
            var value = new int[count];
            fixed (int* v = &value[0])
            {
                _glGetIntegerv(paramName, v);
            }
            return value;
        }

        public static void glGetInteger64v(int paramName, long* data) => _glGetInteger64v(paramName, data);


        public static long glGetInteger64(int paramName)
        {
            long value;
            _glGetInteger64v(paramName, &value);
            return value;
        }



        public static long[] glGetInteger64v(int paramName, int count)
        {
            var value = new long[count];
            fixed (long* v = &value[0])
            {
                _glGetInteger64v(paramName, v);
            }
            return value;
        }



        public static float[] glGetTexParameterfv(int target, int paramName, int count)
        {
            var args = new float[count];
            fixed (float* a = &args[0])
            {
                _glGetTexParameterfv(target, paramName, a);
            }
            return args;
        }



        public static int[] glGetTexParameteriv(int target, int paramName, int count)
        {
            var args = new int[count];
            fixed (int* a = &args[0])
            {
                _glGetTexParameteriv(target, paramName, a);
            }
            return args;
        }

        public static void glGetTexParameterfv(int target, int paramName, float* args) => _glGetTexParameterfv(target, paramName, args);

        public static void glGetTexParameteriv(int target, int paramName, int* args) => _glGetTexParameteriv(target, paramName, args);

        public static float glGetTexParameterf(int target, int paramName)
        {
            float args;
            _glGetTexParameterfv(target, paramName, &args);
            return args;
        }

        public static int glGetTexParameteri(int target, int paramName)
        {
            int args;
            _glGetTexParameteriv(target, paramName, &args);
            return args;
        }



        public static float[] glGetTexLevelParameterfv(int target, int level, int paramName, int count)
        {
            var args = new float[count];
            fixed (float* a = &args[0])
            {
                _glGetTexLevelParameterfv(target, level, paramName, a);
            }
            return args;
        }



        public static int[] glGetTexLevelParameteriv(int target, int level, int paramName, int count)
        {
            var args = new int[count];
            fixed (int* a = &args[0])
            {
                _glGetTexLevelParameteriv(target, level, paramName, a);
            }
            return args;
        }

        public static void glGetTexLevelParameterfv(int target, int level, int paramName, float* args) => _glGetTexLevelParameterfv(target, level, paramName, args);

        public static void glGetTexLevelParameteriv(int target, int level, int paramName, int* args) => _glGetTexLevelParameteriv(target, level, paramName, args);

        public static float glGetTexLevelParameterf(int target, int level, int paramName)
        {
            float args;
            _glGetTexLevelParameterfv(target, level, paramName, &args);
            return args;
        }

        public static int glGetTexLevelParameteri(int target, int level, int paramName)
        {
            int args;
            _glGetTexLevelParameteriv(target, level, paramName, &args);
            return args;
        }

        public static void glCopyTexImage1D(int target, int level, int internalFormat, int x, int y, int width, int border) => _glCopyTexImage1D(target, level, internalFormat, x, y, width, border);

        public static void glCopyTexImage2D(int target, int level, int internalFormat, int x, int y, int width, int height, int border) => _glCopyTexImage2D(target, level, internalFormat, x, y, width, height, border);

        public static void glCopyTexSubImage1D(int target, int level, int xOffset, int x, int y, int width) => _glCopyTexSubImage1D(target, level, xOffset, x, y, width);

        public static void glCopyTexSubImage2D(int target, int level, int xOffset, int yOffset, int x, int y, int width, int height) => _glCopyTexSubImage2D(target, level, xOffset, yOffset, x, y, width, height);

        public static void glTexSubImage1D(int target, int level, int xOffset, int width, int format, int type, IntPtr pixels) => _glTexSubImage1D(target, level, xOffset, width, format, type, pixels.ToPointer());

        public static void glTexSubImage2D(int target, int level, int xOffset, int yOffset, int width, int height, int format, int type, IntPtr pixels) => _glTexSubImage2D(target, level, xOffset, yOffset, width, height, format, type, pixels.ToPointer());

        public static void glTexSubImage3D(int target, int level, int xOffset, int yOffset, int zOffset, int width,
            int height, int depth, int format, int type, IntPtr pixels) => _glTexSubImage3D(target, level,
            xOffset, yOffset, zOffset, width, height, depth, format, type, pixels.ToPointer());

        public static void glTexSubImage1D(int target, int level, int xOffset, int width, int format, int type, /*const*/ void* pixels) => _glTexSubImage1D(target, level, xOffset, width, format, type, pixels);

        public static void glTexSubImage2D(int target, int level, int xOffset, int yOffset, int width, int height, int format, int type, /*const*/ void* pixels) => _glTexSubImage2D(target, level, xOffset, yOffset, width, height, format, type, pixels);

        public static void glTexSubImage3D(int target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int format, int type, /*const*/ void* pixels) => _glTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, type, pixels);

        public static void glDrawRangeElements(int mode, uint start, uint end, int count, byte[] indices)
        {
            fixed (void* i = &indices[0])
            {
                _glDrawRangeElements(mode, start, end, count, GlfwConstants.GL_UNSIGNED_BYTE, i);
            }
        }

        public static void glDrawRangeElements(int mode, uint start, uint end, int count, ushort[] indices)
        {
            fixed (void* i = &indices[0])
            {
                _glDrawRangeElements(mode, start, end, count, GlfwConstants.GL_UNSIGNED_SHORT, i);
            }
        }

        public static void glDrawRangeElements(int mode, uint start, uint end, int count, uint[] indices)
        {
            fixed (void* i = &indices[0])
            {
                _glDrawRangeElements(mode, start, end, count, GlfwConstants.GL_UNSIGNED_INT, i);
            }
        }

        public static void glDrawRangeElements(int mode, uint start, uint end, int count, int type, /*const*/void* indices) => _glDrawRangeElements(mode, start, end, count, type, indices);

        public static IntPtr glMapBuffer(int target, int access) => new IntPtr(_glMapBuffer(target, access));

        public static bool glUnmapBuffer(int target) => _glUnmapBuffer(target);

        public static void glCopyTexSubImage3D(int target, int level, int xOffset, int yOffset, int zOffset, int x, int y, int width, int height) => _glCopyTexSubImage3D(target, level, xOffset, yOffset, zOffset, x, y, width, height);

        public static void glCompressedTexImage3D(int target, int level, int internalFormat, int width, int height, int depth, int border, int imageSize, IntPtr data) => _glCompressedTexImage3D(target, level, internalFormat, width, height, depth, border, imageSize, data.ToPointer());

        public static void glCompressedTexImage2D(int target, int level, int internalFormat, int width, int height, int border, int imageSize, IntPtr data) => _glCompressedTexImage2D(target, level, internalFormat, width, height, border, imageSize, data.ToPointer());

        public static void glCompressedTexImage1D(int target, int level, int internalFormat, int width, int border, int imageSize, IntPtr data) => _glCompressedTexImage1D(target, level, internalFormat, width, border, imageSize, data.ToPointer());

        public static void glCompressedTexImage3D(int target, int level, int internalFormat, int width, int height, int depth, int border, int imageSize, /*const*/ void* data) => _glCompressedTexImage3D(target, level, internalFormat, width, height, depth, border, imageSize, data);

        public static void glCompressedTexImage2D(int target, int level, int internalFormat, int width, int height, int border, int imageSize, /*const*/ void* data) => _glCompressedTexImage2D(target, level, internalFormat, width, height, border, imageSize, data);

        public static void glCompressedTexImage1D(int target, int level, int internalFormat, int width, int border, int imageSize, /*const*/ void* data) => _glCompressedTexImage1D(target, level, internalFormat, width, border, imageSize, data);

        public static void glCompressedTexSubImage3D(int target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int format, int imageSize, IntPtr data) => _glCompressedTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, imageSize, data.ToPointer());

        public static void glCompressedTexSubImage2D(int target, int level, int xOffset, int yOffset, int width, int height, int format, int imageSize, IntPtr data) => _glCompressedTexSubImage2D(target, level, xOffset, yOffset, width, height, format, imageSize, data.ToPointer());

        public static void glCompressedTexSubImage1D(int target, int level, int xOffset, int width, int format, int imageSize, IntPtr data) => _glCompressedTexSubImage1D(target, level, xOffset, width, format, imageSize, data.ToPointer());

        public static void glCompressedTexSubImage3D(int target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int format, int imageSize, /*const*/ void* data) => _glCompressedTexSubImage3D(target, level, xOffset, yOffset, zOffset, width, height, depth, format, imageSize, data);

        public static void glCompressedTexSubImage2D(int target, int level, int xOffset, int yOffset, int width, int height, int format, int imageSize, /*const*/ void* data) => _glCompressedTexSubImage2D(target, level, xOffset, yOffset, width, height, format, imageSize, data);

        public static void glCompressedTexSubImage1D(int target, int level, int xOffset, int width, int format, int imageSize, /*const*/ void* data) => _glCompressedTexSubImage1D(target, level, xOffset, width, format, imageSize, data);

        public static void glBlendFuncSeparate(int sFactorRgb, int dFactorRgb, int sFactorAlpha, int dFactorAlpha) => _glBlendFuncSeparate(sFactorRgb, dFactorRgb, sFactorAlpha, dFactorAlpha);

        public static void glDeleteFramebuffers(int n, /*const*/ uint* buffers) => _glDeleteFramebuffers(n, buffers);

        public static void glDeleteFramebuffers(uint[] buffers)
        {
            if (buffers is null)
                return;
            fixed (uint* ids = &buffers[0])
            {
                _glDeleteFramebuffers(buffers.Length, ids);
            }
        }

        public static void glDeleteFramebuffer(uint buffer) => _glDeleteFramebuffers(1, &buffer);

        public static void glBindBuffer(int target, uint buffer) => _glBindBuffer(target, buffer);

        public static void glBindFramebuffer(uint framebuffer) => _glBindFramebuffer(GlfwConstants.GL_FRAMEBUFFER, framebuffer);

        public static void glBindRenderbuffer(uint renderbuffer) => _glBindRenderbuffer(GlfwConstants.GL_RENDERBUFFER, renderbuffer);

        public static void glDeleteBuffer(uint buffer) => _glDeleteBuffers(1, &buffer);

        public static void glDeleteRenderbuffer(uint renderbuffer) => _glDeleteRenderbuffers(1, &renderbuffer);

        public static void glDeleteRenderbuffers(int n, /*const*/ uint* buffers) => _glDeleteRenderbuffers(n, buffers);

        public static void glDeleteRenderbuffers(uint[] buffers)
        {
            if (buffers is null)
                return;
            fixed (uint* ids = &buffers[0])
            {
                _glDeleteRenderbuffers(buffers.Length, ids);
            }
        }

        public static void glDeleteBuffers(int n, /*const*/ uint* buffers) => _glDeleteBuffers(n, buffers);

        public static void glDeleteBuffers(uint[] buffers)
        {
            if (buffers is null)
                return;
            fixed (uint* ids = &buffers[0])
            {
                _glDeleteBuffers(buffers.Length, ids);
            }
        }

        public static void glGenFramebuffers(int n, uint* buffers) => _glGenFramebuffers(n, buffers);


        public static uint glGenFramebuffer()
        {
            uint id;
            _glGenFramebuffers(1, &id);
            return id;
        }



        public static uint[] glGenFramebuffers(int n)
        {
            var buffers = new uint[n];
            fixed (uint* ids = &buffers[0])
            {
                _glGenFramebuffers(n, ids);
            }
            return buffers;
        }

        public static void glGenRenderbuffers(int n, uint* buffers) => _glGenRenderbuffers(n, buffers);


        public static uint glGenRenderbuffer()
        {
            uint id;
            _glGenRenderbuffers(1, &id);
            return id;
        }



        public static uint[] glGenRenderbuffers(int n)
        {
            var buffers = new uint[n];
            fixed (uint* ids = &buffers[0])
            {
                _glGenRenderbuffers(n, ids);
            }
            return buffers;
        }

        public static void glGenBuffers(int n, uint* buffers) => _glGenBuffers(n, buffers);


        public static uint glGenBuffer()
        {
            uint id;
            _glGenBuffers(1, &id);
            return id;
        }



        public static uint[] glGenBuffers(int n)
        {
            var buffers = new uint[n];
            fixed (uint* ids = &buffers[0])
            {
                _glGenBuffers(n, ids);
            }
            return buffers;
        }

        public static bool glIsBuffer(uint buffer) => _glIsBuffer(buffer);

        public static bool glIsFramebuffer(uint framebuffer) => _glIsFramebuffer(framebuffer);

        public static bool glIsRenderbuffer(uint renderbuffer) => _glIsRenderbuffer(renderbuffer);

        public static void glGenSamplers(int count, uint* samplers) => _glGenSamplers(count, samplers);


        public static uint[] glGenSamplers(int count)
        {
            var samplers = new uint[count];
            fixed (uint* s = &samplers[0])
            {
                _glGenSamplers(count, s);
            }

            return samplers;
        }

        public static uint glGenSampler()
        {
            uint sampler;
            _glGenSamplers(1, &sampler);
            return sampler;
        }

        public static bool glIsSampler(uint sampler) => _glIsSampler(sampler);

        public static void glDeleteSamplers(uint[] samplers)
        {
            fixed (uint* s = &samplers[0])
            {
                _glDeleteSamplers(samplers.Length, s);
            }
        }

        public static void glDeleteSamplers(int count, /*const*/ uint* samplers) => _glDeleteSamplers(count, samplers);

        public static void glDeleteSampler(uint sampler) => _glDeleteSamplers(1, &sampler);

        public static void glBindSampler(uint unit, uint sampler) => _glBindSampler(unit, sampler);

        public static void glFramebufferTexture1D(int target, int attachment, int texTarget, uint texture, int level) => _glFramebufferTexture1D(target, attachment, texTarget, texture, level);

        public static void glFramebufferTexture2D(int target, int attachment, int texTarget, uint texture, int level) => _glFramebufferTexture2D(target, attachment, texTarget, texture, level);

        public static void glFramebufferTexture3D(int target, int attachment, int texTarget, uint texture, int level, int zOffset) => _glFramebufferTexture3D(target, attachment, texTarget, texture, level, zOffset);

        public static int glCheckFramebufferStatus(int target) => _glCheckFramebufferStatus(target);

        public static void glClearBufferiv(int buffer, int drawbuffer, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glClearBufferiv(buffer, drawbuffer, v);
            }
        }

        public static void glClearBufferuiv(int buffer, int drawbuffer, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glClearBufferuiv(buffer, drawbuffer, v);
            }
        }

        public static void glClearBufferfv(int buffer, int drawbuffer, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glClearBufferfv(buffer, drawbuffer, v);
            }
        }

        public static void glClearBufferiv(int buffer, int drawbuffer, /*const*/ int* value) => _glClearBufferiv(buffer, drawbuffer, value);

        public static void glClearBufferuiv(int buffer, int drawbuffer, /*const*/ uint* value) => _glClearBufferuiv(buffer, drawbuffer, value);

        public static void glClearBufferfv(int buffer, int drawbuffer, /*const*/ float* value) => _glClearBufferfv(buffer, drawbuffer, value);

        public static void glClearBufferfi(int buffer, int drawbuffer, float depth, int stencil) => _glClearBufferfi(buffer, drawbuffer, depth, stencil);

        public static void glAttachShader(uint program, uint shader) => _glAttachShader(program, shader);

        public static void glBindBufferBase(int target, uint index, uint buffer) => _glBindBufferBase(target, index, buffer);

        public static void glQueryCounter(uint id, int target) => _glQueryCounter(id, target);

        public static void glSampleMaski(uint maskNumber, uint mask) => _glSampleMaski(maskNumber, mask);

        public static int glGetFragDataIndex(uint program, string name)
        {
            var buffer = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &buffer[0])
            {
                return _glGetFragDataIndex(program, b);
            }
        }

        public static void glBeginTransformFeedback(int primitiveMode) => _glBeginTransformFeedback(primitiveMode);

        public static void glEndTransformFeedback() => _glEndTransformFeedback();

        public static void glEnablei(int target, uint index) => _glEnablei(target, index);

        public static void glDisablei(int target, uint index) => _glDisablei(target, index);

        public static bool glIsEnabledi(int target, uint index) => _glIsEnabledi(target, index);

        public static void glCompileShader(uint shader) => _glCompileShader(shader);

        public static uint glCreateProgram() => _glCreateProgram();

        public static uint glCreateShader(int type) => _glCreateShader(type);

        public static bool glIsProgram(uint program) => _glIsProgram(program);

        public static bool glIsShader(uint shader) => _glIsShader(shader);

        public static void glDeleteProgram(uint program) => _glDeleteProgram(program);

        public static void glDeleteShader(uint shader) => _glDeleteShader(shader);

        public static void glDetachShader(uint program, uint shader) => _glDetachShader(program, shader);

        public static void glUseProgram(uint program) => _glUseProgram(program);

        public static void glLinkProgram(uint program) => _glLinkProgram(program);

        public static void glShaderSource(uint shader, int count, /*const*/ byte** str, /*const*/ int* length) => _glShaderSource(shader, count, str, length);

        public static void glShaderSource(uint shader, string source)
        {
            var buffer = Encoding.UTF8.GetBytes(source);
            fixed (byte* p = &buffer[0])
            {
                var sources = new[] { p };
                fixed (byte** s = &sources[0])
                {
                    var length = buffer.Length;
                    _glShaderSource(shader, 1, s, &length);
                }
            }
        }

        public static int glGetUniformLocation(uint program, /*const*/ byte* name) => _glGetUniformLocation(program, name);

        public static int glGetUniformLocation(uint program, byte[] name)
        {
            fixed (byte* b = &name[0])
            {
                return _glGetUniformLocation(program, b);
            }
        }

        public static int glGetUniformLocation(uint program, string name)
        {
            var bytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &bytes[0])
            {
                return _glGetUniformLocation(program, b);
            }
        }



        public static string glGetShaderSource(uint shader, int bufSize = 4096)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            try
            {
                int length;
                var source = (byte*)buffer.ToPointer();
                _glGetShaderSource(shader, bufSize, &length, source);
                return PtrToStringUtf8(buffer, length);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }



        public static string glGetProgramInfoLog(uint program, int bufSize = 1024)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            try
            {
                int length;
                var source = (byte*)buffer.ToPointer();
                _glGetProgramInfoLog(program, bufSize, &length, source);
                return PtrToStringUtf8(buffer, length);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }



        public static string glGetShaderInfoLog(uint shader, int bufSize = 1024)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            try
            {
                int length;
                var source = (byte*)buffer.ToPointer();
                _glGetShaderInfoLog(shader, bufSize, &length, source);
                return PtrToStringUtf8(buffer, length);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static void glValidateProgram(uint program) => _glValidateProgram(program);

        public static void glMultiDrawElements(int mode, /*const*/ int* count, int type, /*const*/void* /*const*/* indices, int drawCount) => _glMultiDrawElements(mode, count, type, indices, drawCount);

        public static void glMultiDrawElements(int mode, int[] count, int type, IntPtr indices, int drawCount)
        {
            // Test this actually works
            var ptr = (void**)indices.ToPointer();
            fixed (int* c = &count[0])
            {
                _glMultiDrawElements(mode, c, type, ptr, drawCount);
            }
        }

        public static void glMultiDrawArrays(int mode, /*const*/ int* first, /*const*/ int* count, int drawCount) => _glMultiDrawArrays(mode, first, count, drawCount);

        public static void glMultiDrawArrays(int mode, int[] first, int[] count, int drawCount)
        {
            fixed (int* f = &first[0])
            {
                fixed (int* c = &count[0])
                {
                    _glMultiDrawArrays(mode, f, c, drawCount);
                }
            }
        }

        public static void glFramebufferTexture(int target, int attachment, uint texture, int level) => _glFramebufferTexture(target, attachment, texture, level);

        public static void glFramebufferRenderbuffer(int target, int attachment, int renderbufferTarget, uint renderbuffer) => _glFramebufferRenderbuffer(target, attachment, renderbufferTarget, renderbuffer);

        public static void glFramebufferRenderbuffer(int attachment, uint renderbuffer) => _glFramebufferRenderbuffer(GlfwConstants.GL_FRAMEBUFFER, attachment, GlfwConstants.GL_RENDERBUFFER, renderbuffer);

        public static void glGetBufferSubData(int target, int offset, int size, IntPtr data) => _glGetBufferSubData(target, new IntPtr(offset), new IntPtr(size), data.ToPointer());

        public static void glGetBufferSubData(int target, long offset, long size, IntPtr data) => _glGetBufferSubData(target, new IntPtr(offset), new IntPtr(size), data.ToPointer());

        public static void glGetBufferSubData(int target, int offset, int size, void* data) => _glGetBufferSubData(target, new IntPtr(offset), new IntPtr(size), data);

        public static void glGetBufferSubData(int target, long offset, long size, void* data) => _glGetBufferSubData(target, new IntPtr(offset), new IntPtr(size), data);

        public static IntPtr glMapBufferRange(int target, int offset, int length, uint access) => new IntPtr(_glMapBufferRange(target, new IntPtr(offset), new IntPtr(length), access));

        public static IntPtr glMapBufferRange(int target, long offset, long length, uint access) => new IntPtr(_glMapBufferRange(target, new IntPtr(offset), new IntPtr(length), access));

        public static void glFlushMappedBufferRange(int target, int offset, int length) => _glFlushMappedBufferRange(target, new IntPtr(offset), new IntPtr(length));

        public static void glFlushMappedBufferRange(int target, long offset, long length) => _glFlushMappedBufferRange(target, new IntPtr(offset), new IntPtr(length));

        public static void glFramebufferTextureLayer(int target, int attachment, uint texture, int level, int layer) => _glFramebufferTextureLayer(target, attachment, texture, level, layer);

        public static void glBindBufferRange(int target, uint index, uint buffer, int offset, int size) => _glBindBufferRange(target, index, buffer, new IntPtr(offset), new IntPtr(size));

        public static void glBindBufferRange(int target, uint index, uint buffer, long offset, long size) => _glBindBufferRange(target, index, buffer, new IntPtr(offset), new IntPtr(size));

        public static void glBlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, int filter) => _glBlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);

        public static void glRenderbufferStorage(int target, int internalFormat, int width, int height) => _glRenderbufferStorage(target, internalFormat, width, height);

        public static void glColorP3ui(int type, uint color) => _glColorP3ui(type, color);

        public static void glColorP3uiv(int type, /*const*/ uint* color) => _glColorP3uiv(type, color);

        public static void glColorP4ui(int type, uint color) => _glColorP4ui(type, color);

        public static void glColorP4uiv(int type, /*const*/ uint* color) => _glColorP4uiv(type, color);

        public static void glColorP3uiv(int type, uint[] color)
        {
            fixed (uint* c = &color[0])
            {
                _glColorP3uiv(type, c);
            }
        }

        public static void glColorP4uiv(int type, uint[] color)
        {
            fixed (uint* c = &color[0])
            {
                _glColorP4uiv(type, c);
            }
        }

        public static void glSecondaryColorP3ui(int type, uint color) => _glSecondaryColorP3ui(type, color);

        public static void glSecondaryColorP3uiv(int type, uint[] color)
        {
            fixed (uint* c = &color[0])
            {
                _glSecondaryColorP3uiv(type, c);
            }
        }

        public static void glSecondaryColorP3uiv(int type, /*const*/ uint* color) => _glSecondaryColorP3uiv(type, color);

        public static void glBufferSubData(int target, int offset, int size, IntPtr data) => _glBufferSubData(target, new IntPtr(offset), new IntPtr(size), data.ToPointer());

        public static void glBufferSubData(int target, long offset, long size, IntPtr data) => _glBufferSubData(target, new IntPtr(offset), new IntPtr(size), data.ToPointer());


        public static void glBufferSubData(int target, int offset, int size, /*const*/ void* data) => _glBufferSubData(target, new IntPtr(offset), new IntPtr(size), data);

        public static void glBufferSubData(int target, long offset, long size, /*const*/ void* data) => _glBufferSubData(target, new IntPtr(offset), new IntPtr(size), data);

        public static void glNormalP3ui(int type, uint coords) => _glNormalP3ui(type, coords);

        public static void glNormalP3uiv(int type, /*const*/ uint* coords) => _glNormalP3uiv(type, coords);

        public static void glNormalP3uiv(int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glNormalP3uiv(type, c);
            }
        }

        public static void glBindFragDataLocation(uint program, uint color, string name)
        {
            var utf8 = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &utf8[0])
            {
                _glBindFragDataLocation(program, color, b);
            }
        }


        public static int glGetFragDataLocation(uint program, string name)
        {
            var utf8 = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &utf8[0])
            {
                return _glGetFragDataLocation(program, b);
            }
        }

        public static int glGetAttribLocation(uint program, string name)
        {
            var utf8 = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &utf8[0])
            {
                return _glGetAttribLocation(program, b);
            }
        }

        public static void glGetAttachedShaders(uint program, int maxCount, int* count, uint* shaders) => _glGetAttachedShaders(program, maxCount, count, shaders);



        public static uint[] glGetAttachedShaders(uint program, int maxCount)
        {
            int count;
            var shaders = new uint[maxCount];
            fixed (uint* shader = &shaders[0])
            {
                _glGetAttachedShaders(program, maxCount, &count, shader);
            }
            return count < maxCount ? shaders.Take(count).ToArray() : shaders;
        }

        public static void glBindAttribLocation(uint program, uint index, string name)
        {
            var utf8 = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &utf8[0])
            {
                _glBindAttribLocation(program, index, b);
            }
        }

        public static void glGetActiveAttrib(uint program, uint index, int bufSize, out int length, out int size,
            out int type, out string name)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            try
            {
                _glGetActiveAttrib(program, index, bufSize, out length, out size, out type, buffer);
                name = PtrToStringUtf8(buffer, length);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static void glGetActiveUniform(uint program, uint index, int bufSize, out int length, out int size,
            out int type, out string name)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            try
            {
                _glGetActiveUniform(program, index, bufSize, out length, out size, out type, buffer);
                name = PtrToStringUtf8(buffer, length);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static void glGenerateMipmap(int target) => _glGenerateMipmap(target);

        public static void glGetBooleani_v(int target, uint index, bool* data) => _glGetBooleani_v(target, index, data);

        public static void glGetIntegeri_v(int target, uint index, int* data) => _glGetIntegeri_v(target, index, data);

        public static void glGetInteger64i_v(int target, uint index, long* data) => _glGetInteger64i_v(target, index, data);

        public static bool glGetBooleani(int target, uint index)
        {
            bool value;
            _glGetBooleani_v(target, index, &value);
            return value;
        }

        public static int glGetIntegeri(int target, uint index)
        {
            int value;
            _glGetIntegeri_v(target, index, &value);
            return value;
        }

        public static long glGetInteger64i(int target, uint index)
        {
            long value;
            _glGetInteger64i_v(target, index, &value);
            return value;
        }


        public static bool[] glGetBooleani_v(int target, uint index, int count)
        {
            var value = new bool[count];
            fixed (bool* v = &value[0])
            {
                _glGetBooleani_v(target, index, v);
            }
            return value;
        }


        public static int[] glGetIntegeri_v(int target, uint index, int count)
        {
            var value = new int[count];
            fixed (int* v = &value[0])
            {
                _glGetIntegeri_v(target, index, v);
            }
            return value;
        }


        public static long[] glGetInteger64i_v(int target, uint index, int count)
        {
            var value = new long[count];
            fixed (long* v = &value[0])
            {
                _glGetInteger64i_v(target, index, v);
            }
            return value;
        }

        public static bool glIsVertexArray(uint array) => _glIsVertexArray(array);

        public static void glGenVertexArrays(int n, uint* arrays) => _glGenVertexArrays(n, arrays);



        public static uint[] glGenVertexArrays(int n)
        {
            var arrays = new uint[n];
            fixed (uint* names = &arrays[0])
            {
                _glGenVertexArrays(n, names);
            }

            return arrays;
        }


        public static uint glGenVertexArray()
        {
            uint array;
            _glGenVertexArrays(1, &array);
            return array;
        }

        public static void glBindVertexArray(uint array) => _glBindVertexArray(array);

        public static void glDeleteVertexArrays(int n, /*const*/ uint* arrays) => _glDeleteVertexArrays(n, arrays);

        public static void glDeleteVertexArrays(uint[] arrays)
        {
            if (arrays is null)
                return;
            fixed (uint* names = &arrays[0])
            {
                _glDeleteVertexArrays(arrays.Length, names);
            }
        }

        public static void glDeleteVertexArray(uint array) => _glDeleteVertexArrays(1, &array);

        public static void glPointParameterf(int paramName, float param) => _glPointParameterf(paramName, param);

        public static void glPointParameteri(int paramName, int param) => _glPointParameteri(paramName, param);

        public static void glPointParameterfv(int paramName, /*const*/ float* args) => _glPointParameterfv(paramName, args);

        public static void glPointParameteriv(int paramName, /*const*/ int* args) => _glPointParameteriv(paramName, args);

        public static void glPointParameterfv(int paramName, float[] args)
        {
            fixed (float* a = &args[0])
            {
                _glPointParameterfv(paramName, a);
            }
        }

        public static void glPointParameteriv(int paramName, int[] args)
        {
            fixed (int* a = &args[0])
            {
                _glPointParameteriv(paramName, a);
            }
        }

        public static void glSamplerParameteri(uint sampler, int paramName, int param) => _glSamplerParameteri(sampler, paramName, param);

        public static void glSamplerParameterf(uint sampler, int paramName, float param) => _glSamplerParameterf(sampler, paramName, param);

        public static void glSamplerParameteriv(uint sampler, int paramName, int[] param)
        {
            fixed (int* p = &param[0])
            {
                _glSamplerParameteriv(sampler, paramName, p);
            }
        }

        public static void glSamplerParameterfv(uint sampler, int paramName, float[] param)
        {
            fixed (float* p = &param[0])
            {
                _glSamplerParameterfv(sampler, paramName, p);
            }
        }

        public static void glSamplerParameteriv(uint sampler, int paramName, /*const*/ int* param) => _glSamplerParameteriv(sampler, paramName, param);

        public static void glSamplerParameterfv(uint sampler, int paramName, /*const*/ float* param) => _glSamplerParameterfv(sampler, paramName, param);

        public static void glSamplerParameterIiv(uint sampler, int paramName, /*const*/ int* param) => _glSamplerParameterIiv(sampler, paramName, param);

        public static void glSamplerParameterIuiv(uint sampler, int paramName, /*const*/ uint* param) => _glSamplerParameterIuiv(sampler, paramName, param);

        public static void glSamplerParameterIiv(uint sampler, int paramName, int[] param)
        {
            fixed (int* p = &param[0])
            {
                _glSamplerParameterIiv(sampler, paramName, p);
            }
        }

        public static void glSamplerParameterIuiv(uint sampler, int paramName, uint[] param)
        {
            fixed (uint* p = &param[0])
            {
                _glSamplerParameterIuiv(sampler, paramName, p);
            }
        }

        public static void glUniform1f(int location, float v0) => _glUniform1f(location, v0);

        public static void glUniform2f(int location, float v0, float v1) => _glUniform2f(location, v0, v1);

        public static void glUniform3f(int location, float v0, float v1, float v2) => _glUniform3f(location, v0, v1, v2);

        public static void glUniform4f(int location, float v0, float v1, float v2, float v3) => _glUniform4f(location, v0, v1, v2, v3);

        public static void glUniform1ui(int location, uint v0) => _glUniform1ui(location, v0);

        public static void glUniform2ui(int location, uint v0, uint v1) => _glUniform2ui(location, v0, v1);

        public static void glUniform3ui(int location, uint v0, uint v1, uint v2) => _glUniform3ui(location, v0, v1, v2);

        public static void glUniform4ui(int location, uint v0, uint v1, uint v2, uint v3) => _glUniform4ui(location, v0, v1, v2, v3);

        public static void glUniform1i(int location, int v0) => _glUniform1i(location, v0);

        public static void glUniform2i(int location, int v0, int v1) => _glUniform2i(location, v0, v1);

        public static void glUniform3i(int location, int v0, int v1, int v2) => _glUniform3i(location, v0, v1, v2);

        public static void glUniform4i(int location, int v0, int v1, int v2, int v3) => _glUniform4i(location, v0, v1, v2, v3);

        public static void glUniform1fv(int location, int count, /*const*/ float* value) => _glUniform1fv(location, count, value);

        public static void glUniform2fv(int location, int count, /*const*/ float* value) => _glUniform2fv(location, count, value);

        public static void glUniform3fv(int location, int count, /*const*/ float* value) => _glUniform3fv(location, count, value);

        public static void glUniform4fv(int location, int count, /*const*/ float* value) => _glUniform4fv(location, count, value);

        public static void glUniform1fv(int location, int count, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glUniform1fv(location, count, v);
            }
        }

        public static void glUniform2fv(int location, int count, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glUniform2fv(location, count, v);
            }
        }

        public static void glUniform3fv(int location, int count, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glUniform3fv(location, count, v);
            }
        }

        public static void glUniform4fv(int location, int count, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glUniform4fv(location, count, v);
            }
        }

        public static void glUniform1iv(int location, int count, /*const*/ int* value) => _glUniform1iv(location, count, value);

        public static void glUniform2iv(int location, int count, /*const*/ int* value) => _glUniform2iv(location, count, value);

        public static void glUniform3iv(int location, int count, /*const*/ int* value) => _glUniform3iv(location, count, value);

        public static void glUniform4iv(int location, int count, /*const*/ int* value) => _glUniform4iv(location, count, value);

        public static void glUniform1iv(int location, int count, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glUniform1iv(location, count, v);
            }
        }

        public static void glUniform2iv(int location, int count, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glUniform2iv(location, count, v);
            }
        }

        public static void glUniform3iv(int location, int count, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glUniform3iv(location, count, v);
            }
        }

        public static void glUniform4iv(int location, int count, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glUniform4iv(location, count, v);
            }
        }

        public static void glUniform1uiv(int location, int count, /*const*/ uint* value) => _glUniform1uiv(location, count, value);

        public static void glUniform2uiv(int location, int count, /*const*/ uint* value) => _glUniform2uiv(location, count, value);

        public static void glUniform3uiv(int location, int count, /*const*/ uint* value) => _glUniform3uiv(location, count, value);

        public static void glUniform4uiv(int location, int count, /*const*/ uint* value) => _glUniform4uiv(location, count, value);

        public static void glUniform1uiv(int location, int count, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glUniform1uiv(location, count, v);
            }
        }

        public static void glUniform2uiv(int location, int count, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glUniform2uiv(location, count, v);
            }
        }

        public static void glUniform3uiv(int location, int count, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glUniform3uiv(location, count, v);
            }
        }

        public static void glUniform4uiv(int location, int count, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glUniform4uiv(location, count, v);
            }
        }

        public static void glMultiTexCoordP1ui(int texture, int type, uint coords) => _glMultiTexCoordP1ui(texture, type, coords);

        public static void glMultiTexCoordP2ui(int texture, int type, uint coords) => _glMultiTexCoordP2ui(texture, type, coords);

        public static void glMultiTexCoordP3ui(int texture, int type, uint coords) => _glMultiTexCoordP3ui(texture, type, coords);

        public static void glMultiTexCoordP4ui(int texture, int type, uint coords) => _glMultiTexCoordP4ui(texture, type, coords);

        public static void glMultiTexCoordP1uiv(int texture, int type, /*const*/ uint* coords) => _glMultiTexCoordP1uiv(texture, type, coords);

        public static void glMultiTexCoordP2uiv(int texture, int type, /*const*/ uint* coords) => _glMultiTexCoordP2uiv(texture, type, coords);

        public static void glMultiTexCoordP3uiv(int texture, int type, /*const*/ uint* coords) => _glMultiTexCoordP3uiv(texture, type, coords);

        public static void glMultiTexCoordP4uiv(int texture, int type, /*const*/ uint* coords) => _glMultiTexCoordP4uiv(texture, type, coords);

        public static void glMultiTexCoordP1uiv(int texture, int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glMultiTexCoordP1uiv(texture, type, c);
            }
        }

        public static void glMultiTexCoordP2uiv(int texture, int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glMultiTexCoordP2uiv(texture, type, c);
            }
        }

        public static void glMultiTexCoordP3uiv(int texture, int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glMultiTexCoordP3uiv(texture, type, c);
            }
        }

        public static void glMultiTexCoordP4uiv(int texture, int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glMultiTexCoordP4uiv(texture, type, c);
            }
        }

        public static void glTexCoordP1ui(int type, uint coords) => _glTexCoordP1ui(type, coords);

        public static void glTexCoordP1uiv(int type, /*const*/ uint* coords) => _glTexCoordP1uiv(type, coords);

        public static void glTexCoordP1uiv(int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glTexCoordP1uiv(type, c);
            }
        }

        public static void glTexCoordP2ui(int type, uint coords) => _glTexCoordP2ui(type, coords);

        public static void glTexCoordP2uiv(int type, /*const*/ uint* coords) => _glTexCoordP2uiv(type, coords);

        public static void glTexCoordP2uiv(int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glTexCoordP2uiv(type, c);
            }
        }

        public static void glTexCoordP3ui(int type, uint coords) => _glTexCoordP3ui(type, coords);

        public static void glTexCoordP3uiv(int type, /*const*/ uint* coords) => _glTexCoordP3uiv(type, coords);

        public static void glTexCoordP3uiv(int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glTexCoordP3uiv(type, c);
            }
        }

        public static void glTexCoordP4ui(int type, uint coords) => _glTexCoordP4ui(type, coords);

        public static void glTexCoordP4uiv(int type, /*const*/ uint* coords) => _glTexCoordP4uiv(type, coords);

        public static void glTexCoordP4uiv(int type, uint[] coords)
        {
            fixed (uint* c = &coords[0])
            {
                _glTexCoordP4uiv(type, c);
            }
        }

        public static void glVertexAttrib1d(uint index, float x) => _glVertexAttrib1d(index, x);

        public static void glVertexAttrib1f(uint index, float x) => _glVertexAttrib1f(index, x);

        public static void glVertexAttrib1s(uint index, short x) => _glVertexAttrib1s(index, x);

        public static void glVertexAttrib2d(uint index, float x, float y) => _glVertexAttrib2d(index, x, y);

        public static void glVertexAttrib2f(uint index, float x, float y) => _glVertexAttrib2f(index, x, y);

        public static void glVertexAttrib2s(uint index, short x, short y) => _glVertexAttrib2s(index, x, y);

        public static void glVertexAttrib3d(uint index, float x, float y, float z) => _glVertexAttrib3d(index, x, y, z);

        public static void glVertexAttrib3f(uint index, float x, float y, float z) => _glVertexAttrib3f(index, x, y, z);

        public static void glVertexAttrib3s(uint index, short x, short y, short z) => _glVertexAttrib3s(index, x, y, z);

        public static void glVertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w) => _glVertexAttrib4Nub(index, x, y, z, w);

        public static void glVertexAttrib4d(uint index, float x, float y, float z, float w) => _glVertexAttrib4d(index, x, y, z, w);

        public static void glVertexAttrib4f(uint index, float x, float y, float z, float w) => _glVertexAttrib4f(index, x, y, z, w);

        public static void glVertexAttrib4s(uint index, short x, short y, short z, short w) => _glVertexAttrib4s(index, x, y, z, w);

        public static void glDisableVertexAttribArray(uint index) => _glDisableVertexAttribArray(index);

        public static void glEnableVertexAttribArray(uint index) => _glEnableVertexAttribArray(index);

        public static void glPrimitiveRestartIndex(uint index) => _glPrimitiveRestartIndex(index);

        public static void glVertexAttrib1dv(uint index, /*const*/ float* v) => _glVertexAttrib1dv(index, v);

        public static void glVertexAttrib1fv(uint index, /*const*/ float* v) => _glVertexAttrib1fv(index, v);

        public static void glVertexAttrib1sv(uint index, /*const*/ short* v) => _glVertexAttrib1sv(index, v);

        public static void glVertexAttrib2dv(uint index, /*const*/ float* v) => _glVertexAttrib2dv(index, v);

        public static void glVertexAttrib2fv(uint index, /*const*/ float* v) => _glVertexAttrib2fv(index, v);

        public static void glVertexAttrib2sv(uint index, /*const*/ short* v) => _glVertexAttrib2sv(index, v);

        public static void glVertexAttrib3dv(uint index, /*const*/ float* v) => _glVertexAttrib3dv(index, v);

        public static void glVertexAttrib3fv(uint index, /*const*/ float* v) => _glVertexAttrib3fv(index, v);

        public static void glVertexAttrib3sv(uint index, /*const*/ short* v) => _glVertexAttrib3sv(index, v);

        public static void glVertexAttrib4bv(uint index, /*const*/ sbyte* v) => _glVertexAttrib4bv(index, v);

        public static void glVertexAttrib4dv(uint index, /*const*/ float* v) => _glVertexAttrib4dv(index, v);

        public static void glVertexAttrib4fv(uint index, /*const*/ float* v) => _glVertexAttrib4fv(index, v);

        public static void glVertexAttrib4iv(uint index, /*const*/ int* v) => _glVertexAttrib4iv(index, v);

        public static void glVertexAttrib4sv(uint index, /*const*/ short* v) => _glVertexAttrib4sv(index, v);

        public static void glVertexAttrib4ubv(uint index, /*const*/ byte* v) => _glVertexAttrib4ubv(index, v);

        public static void glVertexAttrib4uiv(uint index, /*const*/ uint* v) => _glVertexAttrib4uiv(index, v);

        public static void glVertexAttrib4usv(uint index, /*const*/ ushort* v) => _glVertexAttrib4usv(index, v);

        public static void glVertexAttrib1dv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib1dv(index, v);
            }
        }

        public static void glVertexAttrib1fv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib1fv(index, v);
            }
        }

        public static void glVertexAttrib1sv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttrib1sv(index, v);
            }
        }

        public static void glVertexAttrib2dv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib2dv(index, v);
            }
        }

        public static void glVertexAttrib2fv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib2fv(index, v);
            }
        }

        public static void glVertexAttrib2sv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttrib2sv(index, v);
            }
        }

        public static void glVertexAttrib3dv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib3dv(index, v);
            }
        }

        public static void glVertexAttrib3fv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib3fv(index, v);
            }
        }

        public static void glVertexAttrib3sv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttrib3sv(index, v);
            }
        }

        public static void glVertexAttrib4bv(uint index, sbyte[] value)
        {
            fixed (sbyte* v = &value[0])
            {
                _glVertexAttrib4bv(index, v);
            }
        }

        public static void glVertexAttrib4dv(uint index, float[] value)
        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib4dv(index, v);
            }
        }

        public static void glVertexAttrib4fv(uint index, float[] value)

        {
            fixed (float* v = &value[0])
            {
                _glVertexAttrib4fv(index, v);
            }
        }

        public static void glVertexAttrib4iv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttrib4iv(index, v);
            }
        }

        public static void glVertexAttrib4sv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttrib4sv(index, v);
            }
        }

        public static void glVertexAttrib4ubv(uint index, byte[] value)
        {
            fixed (byte* v = &value[0])
            {
                _glVertexAttrib4ubv(index, v);
            }
        }

        public static void glVertexAttrib4uiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttrib4uiv(index, v);
            }
        }

        public static void glVertexAttrib4usv(uint index, ushort[] value)
        {
            fixed (ushort* v = &value[0])
            {
                _glVertexAttrib4usv(index, v);
            }
        }

        public static void glVertexAttrib4Nbv(uint index, /*const*/ sbyte* v) => _glVertexAttrib4Nbv(index, v);

        public static void glVertexAttrib4Niv(uint index, /*const*/ int* v) => _glVertexAttrib4Niv(index, v);

        public static void glVertexAttrib4Nsv(uint index, /*const*/ short* v) => _glVertexAttrib4Nsv(index, v);

        public static void glVertexAttrib4Nubv(uint index, /*const*/ byte* v) => _glVertexAttrib4Nubv(index, v);

        public static void glVertexAttrib4Nuiv(uint index, /*const*/ uint* v) => _glVertexAttrib4Nuiv(index, v);

        public static void glVertexAttrib4Nusv(uint index, /*const*/ ushort* v) => _glVertexAttrib4Nusv(index, v);

        public static void glVertexAttrib4Nbv(uint index, sbyte[] value)
        {
            fixed (sbyte* v = &value[0])
            {
                _glVertexAttrib4Nbv(index, v);
            }
        }

        public static void glVertexAttrib4Niv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttrib4Niv(index, v);
            }
        }

        public static void glVertexAttrib4Nsv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttrib4Nsv(index, v);
            }
        }

        public static void glVertexAttrib4Nubv(uint index, byte[] value)
        {
            fixed (byte* v = &value[0])
            {
                _glVertexAttrib4Nubv(index, v);
            }
        }

        public static void glVertexAttrib4Nuiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttrib4Nuiv(index, v);
            }
        }

        public static void glVertexAttrib4Nusv(uint index, ushort[] value)
        {
            fixed (ushort* v = &value[0])
            {
                _glVertexAttrib4Nusv(index, v);
            }
        }

        public static void glVertexAttribPointer(uint index, int size, int type, bool normalized, int stride, /*const*/void* pointer) => _glVertexAttribPointer(index, size, type, normalized, stride, pointer);

        public static void glVertexAttribPointer(uint index, int size, int type, bool normalized, int stride, IntPtr pointer) => _glVertexAttribPointer(index, size, type, normalized, stride, pointer.ToPointer());



        public static int[] glGetSamplerParameteriv(uint sampler, int paramName, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetSamplerParameteriv(sampler, paramName, args);
            }

            return values;
        }



        public static int[] glGetSamplerParameterIiv(uint sampler, int paramName, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetSamplerParameterIiv(sampler, paramName, args);
            }

            return values;
        }



        public static float[] glGetSamplerParameterfv(uint sampler, int paramName, int count)
        {
            var values = new float[count];
            fixed (float* args = &values[0])
            {
                _glGetSamplerParameterfv(sampler, paramName, args);
            }

            return values;
        }



        public static uint[] GetSamplerParameterIuiv(uint sampler, int paramName, int count)
        {
            var values = new uint[count];
            fixed (uint* args = &values[0])
            {
                _glGetSamplerParameterIuiv(sampler, paramName, args);
            }

            return values;
        }


        public static int glGetSamplerParameteriv(uint sampler, int paramName)
        {
            int value;
            _glGetSamplerParameteriv(sampler, paramName, &value);
            return value;
        }


        public static int glGetSamplerParameterIiv(uint sampler, int paramName)
        {
            int value;
            _glGetSamplerParameterIiv(sampler, paramName, &value);
            return value;
        }


        public static float glGetSamplerParameterfv(uint sampler, int paramName)
        {
            float value;
            _glGetSamplerParameterfv(sampler, paramName, &value);
            return value;
        }


        public static uint GetSamplerParameterIui(uint sampler, int paramName)
        {
            uint value;
            _glGetSamplerParameterIuiv(sampler, paramName, &value);
            return value;
        }

        public static void glGetSamplerParameteriv(uint sampler, int paramName, int* args) => _glGetSamplerParameteriv(sampler, paramName, args);

        public static void glGetSamplerParameterIiv(uint sampler, int paramName, int* args) => _glGetSamplerParameterIiv(sampler, paramName, args);

        public static void glGetSamplerParameterfv(uint sampler, int paramName, float* args) => _glGetSamplerParameterfv(sampler, paramName, args);

        public static void glGetSamplerParameterIuiv(uint sampler, int paramName, uint* args) => _glGetSamplerParameterIuiv(sampler, paramName, args);

        public static void glVertexAttribI1i(uint index, int x) => _glVertexAttribI1i(index, x);

        public static void glVertexAttribI1ui(uint index, uint x) => _glVertexAttribI1ui(index, x);

        public static void glVertexAttribI2i(uint index, int x, int y) => _glVertexAttribI2i(index, x, y);

        public static void glVertexAttribI2ui(uint index, uint x, uint y) => _glVertexAttribI2ui(index, x, y);

        public static void glVertexAttribI3i(uint index, int x, int y, int z) => _glVertexAttribI3i(index, x, y, z);

        public static void glVertexAttribI3ui(uint index, uint x, uint y, uint z) => _glVertexAttribI3ui(index, x, y, z);

        public static void glVertexAttribI4i(uint index, int x, int y, int z, int w) => _glVertexAttribI4i(index, x, y, z, w);

        public static void glVertexAttribI4ui(uint index, uint x, uint y, uint z, uint w) => _glVertexAttribI4ui(index, x, y, z, w);

        public static void glVertexAttribI1iv(uint index, /*const*/ int* v) => _glVertexAttribI1iv(index, v);

        public static void glVertexAttribI1uiv(uint index, /*const*/ uint* v) => _glVertexAttribI1uiv(index, v);

        public static void glVertexAttribI2iv(uint index, /*const*/ int* v) => _glVertexAttribI2iv(index, v);

        public static void glVertexAttribI2uiv(uint index, /*const*/ uint* v) => _glVertexAttribI2uiv(index, v);

        public static void glVertexAttribI3iv(uint index, /*const*/ int* v) => _glVertexAttribI3iv(index, v);

        public static void glVertexAttribI3uiv(uint index, /*const*/ uint* v) => _glVertexAttribI3uiv(index, v);

        public static void glVertexAttribI4iv(uint index, /*const*/ int* v) => _glVertexAttribI4iv(index, v);

        public static void glVertexAttribI4uiv(uint index, /*const*/ uint* v) => _glVertexAttribI4uiv(index, v);

        public static void glVertexAttribI4bv(uint index, /*const*/ sbyte* v) => _glVertexAttribI4bv(index, v);

        public static void glVertexAttribI4sv(uint index, /*const*/ short* v) => _glVertexAttribI4sv(index, v);

        public static void glVertexAttribI4ubv(uint index, /*const*/ byte* v) => _glVertexAttribI4ubv(index, v);

        public static void glVertexAttribI4usv(uint index, /*const*/ ushort* v) => _glVertexAttribI4usv(index, v);

        public static void glVertexAttribI1iv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttribI1iv(index, v);
            }
        }

        public static void glVertexAttribI1uiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribI1uiv(index, v);
            }
        }

        public static void glVertexAttribI2iv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttribI2iv(index, v);
            }
        }

        public static void glVertexAttribI2uiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribI2uiv(index, v);
            }
        }

        public static void glVertexAttribI3iv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttribI3iv(index, v);
            }
        }

        public static void glVertexAttribI3uiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribI3uiv(index, v);
            }
        }

        public static void glVertexAttribI4iv(uint index, int[] value)
        {
            fixed (int* v = &value[0])
            {
                _glVertexAttribI4iv(index, v);
            }
        }

        public static void glVertexAttribI4uiv(uint index, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribI4uiv(index, v);
            }
        }

        public static void glVertexAttribI4bv(uint index, sbyte[] value)
        {
            fixed (sbyte* v = &value[0])
            {
                _glVertexAttribI4bv(index, v);
            }
        }

        public static void glVertexAttribI4sv(uint index, short[] value)
        {
            fixed (short* v = &value[0])
            {
                _glVertexAttribI4sv(index, v);
            }
        }

        public static void glVertexAttribI4ubv(uint index, byte[] value)
        {
            fixed (byte* v = &value[0])
            {
                _glVertexAttribI4ubv(index, v);
            }
        }

        public static void glVertexAttribI4usv(uint index, ushort[] value)
        {
            fixed (ushort* v = &value[0])
            {
                _glVertexAttribI4usv(index, v);
            }
        }

        public static void glVertexAttribDivisor(uint index, uint divisor) => _glVertexAttribDivisor(index, divisor);

        public static void glVertexP2uiv(int type, /*const*/ uint* value) => _glVertexP2uiv(type, value);

        public static void glVertexP3uiv(int type, /*const*/ uint* value) => _glVertexP3uiv(type, value);

        public static void glVertexP4uiv(int type, /*const*/ uint* value) => _glVertexP4uiv(type, value);

        public static void glVertexP2uiv(int type, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexP2uiv(type, v);
            }
        }

        public static void glVertexP3uiv(int type, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexP3uiv(type, v);
            }
        }

        public static void glVertexP4uiv(int type, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexP4uiv(type, v);
            }
        }

        public static void glVertexP2ui(int type, uint value) => _glVertexP2ui(type, value);

        public static void glVertexP3ui(int type, uint value) => _glVertexP3ui(type, value);

        public static void glVertexP4ui(int type, uint value) => _glVertexP4ui(type, value);

        public static void glVertexAttribP1uiv(uint index, int type, bool normalized, /*const*/ uint* value) => _glVertexAttribP1uiv(index, type, normalized, value);

        public static void glVertexAttribP2uiv(uint index, int type, bool normalized, /*const*/ uint* value) => _glVertexAttribP2uiv(index, type, normalized, value);

        public static void glVertexAttribP3uiv(uint index, int type, bool normalized, /*const*/ uint* value) => _glVertexAttribP3uiv(index, type, normalized, value);

        public static void glVertexAttribP4uiv(uint index, int type, bool normalized, /*const*/ uint* value) => _glVertexAttribP4uiv(index, type, normalized, value);

        public static void glVertexAttribP1uiv(uint index, int type, bool normalized, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribP1uiv(index, type, normalized, v);
            }
        }

        public static void glVertexAttribP2uiv(uint index, int type, bool normalized, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribP2uiv(index, type, normalized, v);
            }
        }

        public static void glVertexAttribP3uiv(uint index, int type, bool normalized, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribP3uiv(index, type, normalized, v);
            }
        }

        public static void glVertexAttribP4uiv(uint index, int type, bool normalized, uint[] value)
        {
            fixed (uint* v = &value[0])
            {
                _glVertexAttribP4uiv(index, type, normalized, v);
            }
        }

        public static void glVertexAttribP1ui(uint index, int type, bool normalized, uint value) => _glVertexAttribP1ui(index, type, normalized, value);

        public static void glVertexAttribP2ui(uint index, int type, bool normalized, uint value) => _glVertexAttribP2ui(index, type, normalized, value);

        public static void glVertexAttribP3ui(uint index, int type, bool normalized, uint value) => _glVertexAttribP3ui(index, type, normalized, value);

        public static void glVertexAttribP4ui(uint index, int type, bool normalized, uint value) => _glVertexAttribP4ui(index, type, normalized, value);


        public static void glTexBuffer(int target, int internalFormat, uint buffer) => _glTexBuffer(target, internalFormat, buffer);

        public static void glGetActiveUniformBlockiv(uint program, uint uniformBlockIndex, int pname, int* args) => _glGetActiveUniformBlockiv(program, uniformBlockIndex, pname, args);



        public static int[] glGetActiveUniformBlockiv(uint program, uint uniformBlockIndex, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetActiveUniformBlockiv(program, uniformBlockIndex, pname, args);
            }
            return values;
        }



        public static string glGetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize = 512)
        {
            int length;
            var buffer = new byte[bufSize];
            fixed (byte* name = &buffer[0])
            {
                _glGetActiveUniformBlockName(program, uniformBlockIndex, bufSize, &length, name);
            }
            return Encoding.UTF8.GetString(buffer, 0, Math.Min(bufSize, length));
        }


        public static void glBindFragDataLocationIndexed(uint program, uint colorNumber, uint index, string name)
        {
            var buffer = Encoding.UTF8.GetBytes(name);
            fixed (byte* b = &buffer[0])
            {
                _glBindFragDataLocationIndexed(program, colorNumber, index, b);
            }
        }

        public static void glGetQueryObjectiv(uint id, int pname, int* args) => _glGetQueryObjectiv(id, pname, args);

        public static void glGetQueryObjectuiv(uint id, int pname, uint* args) => _glGetQueryObjectuiv(id, pname, args);

        public static void glGetQueryObjecti64v(uint id, int pname, long* args) => _glGetQueryObjecti64v(id, pname, args);

        public static void glGetQueryObjectui64v(uint id, int pname, ulong* args) => _glGetQueryObjectui64v(id, pname, args);



        public static ulong[] glGetQueryObjectui64v(uint id, int pname, int count)
        {
            var values = new ulong[count];
            fixed (ulong* args = &values[0])
            {
                _glGetQueryObjectui64v(id, pname, args);
            }
            return values;
        }



        public static long[] glGetQueryObjecti64v(uint id, int pname, int count)
        {
            var values = new long[count];
            fixed (long* args = &values[0])
            {
                _glGetQueryObjecti64v(id, pname, args);
            }
            return values;
        }



        public static uint[] glGetQueryObjectuiv(uint id, int pname, int count)
        {
            var values = new uint[count];
            fixed (uint* args = &values[0])
            {
                _glGetQueryObjectuiv(id, pname, args);
            }
            return values;
        }



        public static int[] glGetQueryObjectiv(uint id, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetQueryObjectiv(id, pname, args);
            }
            return values;
        }



        public static string glGetActiveUniformName(uint program, uint uniformIndex, int bufSize = 512)
        {
            int length;
            var buffer = new byte[bufSize];
            fixed (byte* name = &buffer[0])
            {
                _glGetActiveUniformName(program, uniformIndex, bufSize, &length, name);
            }
            return Encoding.UTF8.GetString(buffer, 0, Math.Min(length, bufSize));
        }

        public static void glBindFramebuffer(int target, uint framebuffer) => _glBindFramebuffer(target, framebuffer);

        public static void glUniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding) => _glUniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);

        public static void glGetProgramiv(uint program, int pname, int* args) => _glGetProgramiv(program, pname, args);



        public static int[] glGetProgramiv(uint program, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetProgramiv(program, pname, args);
            }
            return values;
        }

        public static void glGetShaderiv(uint shader, int pname, int* args) => _glGetShaderiv(shader, pname, args);



        public static int[] glGetShaderiv(uint shader, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetShaderiv(shader, pname, args);
            }
            return values;
        }

        public static void glGetQueryiv(int target, int pname, int* args) => _glGetQueryiv(target, pname, args);



        public static int[] glGetQueryiv(int target, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetQueryiv(target, pname, args);
            }
            return values;
        }

        public static void glGetUniformfv(uint program, int location, float* args) => _glGetUniformfv(program, location, args);



        public static float[] glGetUniformfv(uint program, int location, int count)
        {
            var values = new float[count];
            fixed (float* args = &values[0])
            {
                _glGetUniformfv(program, location, args);
            }
            return values;
        }

        public static void glGetUniformuiv(uint program, int location, uint* args) => _glGetUniformuiv(program, location, args);



        public static uint[] glGetUniformuiv(uint program, int location, int count)
        {
            var values = new uint[count];
            fixed (uint* args = &values[0])
            {
                _glGetUniformuiv(program, location, args);
            }
            return values;
        }

        public static void glGetUniformiv(uint program, int location, int* args) => _glGetUniformiv(program, location, args);



        public static int[] glGetUniformiv(uint program, int location, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetUniformiv(program, location, args);
            }
            return values;
        }

        public static void glCopyBufferSubData(int readTarget, int writeTarget, int readOffset, int writeOffset, int size) => _glCopyBufferSubData(readTarget, writeTarget, new IntPtr(readOffset), new IntPtr(writeOffset), new IntPtr(size));

        public static void glCopyBufferSubData(int readTarget, int writeTarget, long readOffset, long writeOffset, long size) => _glCopyBufferSubData(readTarget, writeTarget, new IntPtr(readOffset), new IntPtr(writeOffset), new IntPtr(size));

        public static void glGetVertexAttribdv(uint index, int pname, float* args) => _glGetVertexAttribdv(index, pname, args);

        public static void glGetVertexAttribfv(uint index, int pname, float* args) => _glGetVertexAttribfv(index, pname, args);

        public static void glGetVertexAttribiv(uint index, int pname, int* args) => _glGetVertexAttribiv(index, pname, args);

        public static void glGetVertexAttribIiv(uint index, int pname, int* args) => _glGetVertexAttribIiv(index, pname, args);

        public static void glGetVertexAttribIuiv(uint index, int pname, uint* args) => _glGetVertexAttribIuiv(index, pname, args);



        public static float[] glGetVertexAttribdv(uint index, int pname, int count)
        {
            var values = new float[count];
            fixed (float* args = &values[0])
            {
                _glGetVertexAttribdv(index, pname, args);
            }
            return values;
        }



        public static float[] glGetVertexAttribfv(uint index, int pname, int count)
        {
            var values = new float[count];
            fixed (float* args = &values[0])
            {
                _glGetVertexAttribfv(index, pname, args);
            }
            return values;
        }



        public static int[] glGetVertexAttribiv(uint index, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetVertexAttribiv(index, pname, args);
            }
            return values;
        }



        public static int[] glGetVertexAttribIiv(uint index, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetVertexAttribIiv(index, pname, args);
            }
            return values;
        }



        public static uint[] glGetVertexAttribIuiv(uint index, int pname, int count)
        {
            var values = new uint[count];
            fixed (uint* args = &values[0])
            {
                _glGetVertexAttribIuiv(index, pname, args);
            }
            return values;
        }

        public static void glVertexAttribIPointer(uint index, int size, int type, int stride, /*const*/ void* pointer) => _glVertexAttribIPointer(index, size, type, stride, pointer);

        public static void glVertexAttribIPointer(uint index, int size, int type, int stride, IntPtr pointer) => _glVertexAttribIPointer(index, size, type, stride, pointer.ToPointer());


        public static void glTexImage2DMultisample(int target, int samples, int internalformat, int width, int height, bool fixedsamplelocations) => _glTexImage2DMultisample(target, samples, internalformat, width, height, fixedsamplelocations);

        public static void glTexImage3DMultisample(int target, int samples, int internalformat, int width, int height, int depth, bool fixedsamplelocations) => _glTexImage3DMultisample(target, samples, internalformat, width, height, depth, fixedsamplelocations);

        public static void glUniformMatrix2fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix2fv(location, count, transpose, value);

        public static void glUniformMatrix3fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix3fv(location, count, transpose, value);

        public static void glUniformMatrix4fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix4fv(location, count, transpose, value);

        public static void glUniformMatrix2x3fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix2x3fv(location, count, transpose, value);

        public static void glUniformMatrix3x2fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix3x2fv(location, count, transpose, value);

        public static void glUniformMatrix2x4fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix2x4fv(location, count, transpose, value);

        public static void glUniformMatrix4x2fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix4x2fv(location, count, transpose, value);

        public static void glUniformMatrix3x4fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix3x4fv(location, count, transpose, value);

        public static void glUniformMatrix4x3fv(int location, int count, bool transpose, /*const*/ float* value) => _glUniformMatrix4x3fv(location, count, transpose, value);

        public static void glUniformMatrix2fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix2fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix3fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix3fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix4fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix4fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix2x3fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix2x3fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix3x2fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix3x2fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix2x4fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix2x4fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix4x2fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix4x2fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix3x4fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix3x4fv(location, count, transpose, value);
            }
        }

        public static void glUniformMatrix4x3fv(int location, int count, bool transpose, float[] values)
        {
            fixed (float* value = &values[0])
            {
                _glUniformMatrix4x3fv(location, count, transpose, value);
            }
        }

        public static void glTexParameterIiv(int target, int pname, /*const*/ int* args) => _glTexParameterIiv(target, pname, args);

        public static void glTexParameterIuiv(int target, int pname, /*const*/ uint* args) => _glTexParameterIuiv(target, pname, args);

        public static void glTexParameterIiv(int target, int pname, int[] args)
        {
            fixed (int* arg = &args[0])
            {
                _glTexParameterIiv(target, pname, arg);
            }
        }

        public static void glTexParameterIuiv(int target, int pname, uint[] args)
        {
            fixed (uint* arg = &args[0])
            {
                _glTexParameterIuiv(target, pname, arg);
            }
        }

        public static void glRenderbufferStorageMultisample(int target, int samples, int internalformat, int width, int height) => _glRenderbufferStorageMultisample(target, samples, internalformat, width, height);

        public static void glDrawArraysInstanced(int mode, int first, int count, int instanceCount) => _glDrawArraysInstanced(mode, first, count, instanceCount);

        public static IntPtr glGetVertexAttribPointerv(uint index, int pname)
        {
            _glGetVertexAttribPointerv(index, pname, out var pointer);
            return pointer;
        }

        public static IntPtr glGetBufferPointerv(int target, int pname)
        {
            _glGetBufferPointerv(target, pname, out var pointer);
            return pointer;
        }

        public static void glGetTexParameterIiv(int target, int pname, int* args) => _glGetTexParameterIiv(target, pname, args);

        public static void glGetTexParameterIuiv(int target, int pname, uint* args) => _glGetTexParameterIuiv(target, pname, args);



        public static int[] glGetTexParameterIiv(int target, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetTexParameterIiv(target, pname, args);
            }
            return values;
        }



        public static uint[] glGetTexParameterIuiv(int target, int pname, int count)
        {
            var values = new uint[count];
            fixed (uint* args = &values[0])
            {
                _glGetTexParameterIuiv(target, pname, args);
            }
            return values;
        }


        public static uint glGetUniformBlockIndex(uint program, string uniformBlockName)
        {
            var buffer = Encoding.UTF8.GetBytes(uniformBlockName);
            fixed (byte* b = &buffer[0])
            {
                return _glGetUniformBlockIndex(program, b);
            }
        }

        public static void glGetActiveUniformsiv(uint program, int uniformCount, /*const*/ uint* uniformIndices, int pname, int* args) => _glGetActiveUniformsiv(program, uniformCount, uniformIndices, pname, args);


        public static void glGetActiveUniformsiv(uint program, int uniformCount, uint[] uniformIndices, int pname, int[] args)
        {
            fixed (uint* i = &uniformIndices[0])
            {
                fixed (int* a = &args[0])
                {
                    _glGetActiveUniformsiv(program, uniformCount, i, pname, a);
                }
            }
        }

        public static void glGetBufferParameteriv(int target, int pname, int* args) => _glGetBufferParameteriv(target, pname, args);



        public static int[] glGetBufferParameteriv(int target, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetBufferParameteriv(target, pname, args);
            }
            return values;
        }

        public static void glGetSynciv(IntPtr sync, int pname, int bufSize, int* length, int* values) => _glGetSynciv(sync, pname, bufSize, length, values);



        public static int[] glGetSynciv(IntPtr sync, int pname, int count, out int length)
        {
            var bufSize = count * sizeof(int);
            var values = new int[count];
            fixed (int* v = &values[0])
            {
                int len;
                _glGetSynciv(sync, pname, bufSize, &len, v);
                length = len;
            }
            return values;
        }

        public static void glGetRenderbufferParameteriv(int target, int pname, int* args) => _glGetRenderbufferParameteriv(target, pname, args);

        public static void glGetRenderbufferParameteriv(int target, int pname, int[] args)
        {
            fixed (int* a = &args[0])
            {
                _glGetRenderbufferParameteriv(target, pname, a);
            }
        }

        public static void glGetMultisamplefv(int pname, uint index, float* val) => _glGetMultisamplefv(pname, index, val);



        public static float[] glGetMultisamplefv(int pname, uint index, int count)
        {
            var values = new float[count];
            fixed (float* val = &values[0])
            {
                _glGetMultisamplefv(pname, index, val);
            }
            return values;
        }

        public static void glDrawElementsInstanced(int mode, int count, int type, /*const*/ void* indices, int instanceCount) => _glDrawElementsInstanced(mode, count, type, indices, instanceCount);


        public static void glDrawElementsInstanced(int mode, int count, byte[] indices, int instanceCount)
        {
            fixed (byte* i = &indices[0])
            {
                _glDrawElementsInstanced(mode, count, GlfwConstants.GL_UNSIGNED_BYTE, i, instanceCount);
            }
        }

        public static void glDrawElementsInstanced(int mode, int count, ushort[] indices, int instanceCount)
        {
            fixed (ushort* i = &indices[0])
            {
                _glDrawElementsInstanced(mode, count, GlfwConstants.GL_UNSIGNED_SHORT, i, instanceCount);
            }
        }

        public static void glDrawElementsInstanced(int mode, int count, uint[] indices, int instanceCount)
        {
            fixed (uint* i = &indices[0])
            {
                _glDrawElementsInstanced(mode, count, GlfwConstants.GL_UNSIGNED_INT, i, instanceCount);
            }
        }

        public static void glDrawElementsBaseVertex(int mode, int count, int type, /*const*/ void* indices, int baseVertex) => _glDrawElementsBaseVertex(mode, count, type, indices, baseVertex);


        public static void glDrawElementsBaseVertex(int mode, int count, byte[] indices, int baseVertex)
        {
            fixed (byte* i = &indices[0])
            {
                glDrawElementsBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_BYTE, i, baseVertex);
            }
        }

        public static void glDrawElementsBaseVertex(int mode, int count, ushort[] indices, int baseVertex)
        {
            fixed (ushort* i = &indices[0])
            {
                glDrawElementsBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_SHORT, i, baseVertex);
            }
        }

        public static void glDrawElementsBaseVertex(int mode, int count, uint[] indices, int baseVertex)
        {
            fixed (uint* i = &indices[0])
            {
                glDrawElementsBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_INT, i, baseVertex);
            }
        }

        public static void glDrawRangeElementsBaseVertex(int mode, uint start, uint end, int count, int type, /*const*/void* indices, int baseVertex) => _glDrawRangeElementsBaseVertex(mode, start, end, count, type, indices, baseVertex);


        public static void glDrawRangeElementsBaseVertex(int mode, uint start, uint end, int count, byte[] indices, int baseVertex)
        {
            fixed (byte* i = &indices[0])
            {
                _glDrawRangeElementsBaseVertex(mode, start, end, count, GlfwConstants.GL_UNSIGNED_BYTE, i, baseVertex);
            }
        }

        public static void glDrawRangeElementsBaseVertex(int mode, uint start, uint end, int count, ushort[] indices, int baseVertex)
        {
            fixed (ushort* i = &indices[0])
            {
                _glDrawRangeElementsBaseVertex(mode, start, end, count, GlfwConstants.GL_UNSIGNED_BYTE, i, baseVertex);
            }
        }

        public static void glDrawRangeElementsBaseVertex(int mode, uint start, uint end, int count, uint[] indices, int baseVertex)
        {
            fixed (uint* i = &indices[0])
            {
                _glDrawRangeElementsBaseVertex(mode, start, end, count, GlfwConstants.GL_UNSIGNED_BYTE, i, baseVertex);
            }
        }

        public static void glDrawElementsInstancedBaseVertex(int mode, int count, int type, /*const*/ void* indices, int instanceCount, int baseVertex) => _glDrawElementsInstancedBaseVertex(mode, count, type, indices, instanceCount, baseVertex);

        public static void glDrawElementsInstancedBaseVertex(int mode, int count, byte[] indices, int instanceCount, int baseVertex)
        {
            fixed (byte* i = &indices[0])
            {
                _glDrawElementsInstancedBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_BYTE, i, instanceCount, baseVertex);
            }
        }

        public static void glDrawElementsInstancedBaseVertex(int mode, int count, ushort[] indices, int instanceCount, int baseVertex)
        {
            fixed (ushort* i = &indices[0])
            {
                _glDrawElementsInstancedBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_SHORT, i, instanceCount, baseVertex);
            }
        }

        public static void glDrawElementsInstancedBaseVertex(int mode, int count, uint[] indices, int instanceCount, int baseVertex)
        {
            fixed (uint* i = &indices[0])
            {
                _glDrawElementsInstancedBaseVertex(mode, count, GlfwConstants.GL_UNSIGNED_INT, i, instanceCount, baseVertex);
            }
        }

        public static uint glGetUniformIndex(uint program, string uniformName)
        {
            uint index;
            var bytes = new[] { Encoding.UTF8.GetBytes(uniformName) };
            fixed (byte* names = &bytes[0][0])
            {
                _glGetUniformIndices(program, 1, &names, &index);
            }
            return index;
        }

        public static void glGetBufferParameteri64v(int target, int pname, long* args) => _glGetBufferParameteri64v(target, pname, args);




        public static long[] glGetBufferParameteri64v(int target, int pname, int count)
        {
            var values = new long[count];
            fixed (long* args = &values[0])
            {
                _glGetBufferParameteri64v(target, pname, args);
            }
            return values;
        }

        public static void glGetTransformFeedbackVarying(uint program, uint index, out int size, out int type, out string name, int bufSize = 512)
        {
            var buffer = Marshal.AllocHGlobal(bufSize);
            _glGetTransformFeedbackVarying(program, index, bufSize, out var length, out size, out type, buffer);
            name = PtrToStringUtf8(buffer, length);
            Marshal.FreeHGlobal(buffer);
        }

        public static void glGetFramebufferAttachmentParameteriv(int target, int attachment, int pname, int* args) => _glGetFramebufferAttachmentParameteriv(target, attachment, pname, args);



        public static int[] glGetFramebufferAttachmentParameteriv(int target, int attachment, int pname, int count)
        {
            var values = new int[count];
            fixed (int* args = &values[0])
            {
                _glGetFramebufferAttachmentParameteriv(target, attachment, pname, args);
            }
            return values;
        }

        public static void glMultiDrawElementsBaseVertex(int mode, /*const*/ int* count, int type, /*const*/void* /*const*/* indices, int drawCount, /*const*/ int* baseVertex) => _glMultiDrawElementsBaseVertex(mode, count, type, indices, drawCount, baseVertex);

        public static void glTransformFeedbackVaryings(uint program, int count, /*const*/ byte** varyings, int bufferMode) => _glTransformFeedbackVaryings(program, count, varyings, bufferMode);










        #endregion External functions
    }
}
