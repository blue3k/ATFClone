//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace Sce.Atf.Windows
{
    public enum MEMORY_RESOURCE_NOTIFICATION_TYPE
    {
        LowMemoryResourceNotification,

        HighMemoryResourceNotification,
    }

    public enum JOBOBJECTINFOCLASS
    {
        /// JobObjectBasicAccountingInformation -> 1
        JobObjectBasicAccountingInformation = 1,

        JobObjectBasicLimitInformation,

        JobObjectBasicProcessIdList,

        JobObjectBasicUIRestrictions,

        JobObjectSecurityLimitInformation,

        JobObjectEndOfJobTimeInformation,

        JobObjectAssociateCompletionPortInformation,

        JobObjectBasicAndIoAccountingInformation,

        JobObjectExtendedLimitInformation,

        JobObjectJobSetInformation,

        MaxJobObjectInfoClass,
    }

    public enum LATENCY_TIME
    {
        LT_DONT_CARE,

        LT_LOWEST_LATENCY,
    }

    public enum HEAP_INFORMATION_CLASS
    {
        HeapCompatibilityInformation,
    }

    public enum GET_FILEEX_INFO_LEVELS
    {
        GetFileExInfoStandard,

        GetFileExMaxInfoLevel,
    }

    public enum COMPUTER_NAME_FORMAT
    {
        ComputerNameNetBIOS,

        ComputerNameDnsHostname,

        ComputerNameDnsDomain,

        ComputerNameDnsFullyQualified,

        ComputerNamePhysicalNetBIOS,

        ComputerNamePhysicalDnsHostname,

        ComputerNamePhysicalDnsDomain,

        ComputerNamePhysicalDnsFullyQualified,

        ComputerNameMax,
    }

    public enum FINDEX_INFO_LEVELS
    {
        FindExInfoStandard,

        FindExInfoMaxInfoLevel,
    }

    public enum FINDEX_SEARCH_OPS
    {
        FindExSearchNameMatch,

        FindExSearchLimitToDirectories,

        FindExSearchLimitToDevices,

        FindExSearchMaxSearchOp,
    }

    public enum WT
    {
        /// WT_EXECUTEDEFAULT -> 0x00000000
        WT_EXECUTEDEFAULT = 0,

        /// WT_EXECUTEINIOTHREAD -> 0x00000001
        WT_EXECUTEINIOTHREAD = 1,

        /// WT_EXECUTEINUITHREAD -> 0x00000002
        WT_EXECUTEINUITHREAD = 2,

        /// WT_EXECUTEINWAITTHREAD -> 0x00000004
        WT_EXECUTEINWAITTHREAD = 4,

        /// WT_EXECUTEONLYONCE -> 0x00000008
        WT_EXECUTEONLYONCE = 8,

        /// WT_EXECUTEINTIMERTHREAD -> 0x00000020
        WT_EXECUTEINTIMERTHREAD = 32,

        /// WT_EXECUTELONGFUNCTION -> 0x00000010
        WT_EXECUTELONGFUNCTION = 16,

        /// WT_EXECUTEINPERSISTENTIOTHREAD -> 0x00000040
        WT_EXECUTEINPERSISTENTIOTHREAD = 64,

        /// WT_EXECUTEINPERSISTENTTHREAD -> 0x00000080
        WT_EXECUTEINPERSISTENTTHREAD = 128,

        /// WT_TRANSFER_IMPERSONATION -> 0x00000100
        WT_TRANSFER_IMPERSONATION = 256,
    }

    public enum SHUTDOWN
    {
        SHUTDOWN_NORETRY = 1
    }

    public enum LGRPID
    {
        LGRPID_INSTALLED = 1,
        LGRPID_SUPPORTED = 2
    }

    public enum CONSOLE_TEXTMODE
    {
        CONSOLE_TEXTMODE_BUFFER = 1
    }

    public enum FIND_ACTCTX_SECTION
    {
        FIND_ACTCTX_SECTION_KEY_RETURN_HACTCTX = 0x00000001,
        FIND_ACTCTX_SECTION_KEY_RETURN_FLAGS = 0x00000002,
        FIND_ACTCTX_SECTION_KEY_RETURN_ASSEMBLY_METADATA = 0x00000004
    }

    public enum ES : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_USER_PRESENT = 0x00000004,
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000
    }

    public enum MOVEFILE
    {
        MOVEFILE_REPLACE_EXISTING = 0x00000001,
        MOVEFILE_COPY_ALLOWED = 0x00000002,
        MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
        MOVEFILE_WRITE_THROUGH = 0x00000008,
        MOVEFILE_CREATE_HARDLINK = 0x00000010,
        MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
    }

    public enum FILE
    {
        FILE_CASE_SENSITIVE_SEARCH = 0x00000001,
        FILE_CASE_PRESERVED_NAMES = 0x00000002,
        FILE_UNICODE_ON_DISK = 0x00000004,
        FILE_PERSISTENT_ACLS = 0x00000008,
        FILE_FILE_COMPRESSION = 0x00000010,
        FILE_VOLUME_QUOTAS = 0x00000020,
        FILE_SUPPORTS_SPARSE_FILES = 0x00000040,
        FILE_SUPPORTS_REPARSE_POINTS = 0x00000080,
        FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100,
        FILE_VOLUME_IS_COMPRESSED = 0x00008000,
        FILE_SUPPORTS_OBJECT_IDS = 0x00010000,
        FILE_SUPPORTS_ENCRYPTION = 0x00020000,
        FILE_NAMED_STREAMS = 0x00040000,
        FILE_READ_ONLY_VOLUME = 0x00080000,
        FILE_SEQUENTIAL_WRITE_ONCE = 0x00100000,
        FILE_SUPPORTS_TRANSACTIONS = 0x00200000,
        FILE_SUPPORTS_HARD_LINKS = 0x00400000,
        FILE_SUPPORTS_EXTENDED_ATTRIBUTES = 0x00800000,
        FILE_SUPPORTS_OPEN_BY_FILE_ID = 0x01000000,
        FILE_SUPPORTS_USN_JOURNAL = 0x02000000
    }

    public enum CONSOLE
    {
        CONSOLE_FULLSCREEN = 1,
        CONSOLE_FULLSCREEN_HARDWARE = 2
    }

    public enum HANDLE_FLAG
    {
        HANDLE_FLAG_INHERIT = 1,
        HANDLE_FLAG_PROTECT_FROM_CLOSE = 2
    }

    public enum CP : ushort
    {
        CP_ACP = 0,
        CP_MACCP = 2,
        CP_OEMCP = 1
    }


    public enum USE_CP
    {
        CP_ACP = 0, // default to ANSI code page
        CP_OEMCP = 1, // default to OEM  code page
        CP_MACCP = 2, // default to MAC  code page
        CP_THREAD_ACP = 3, // current thread's ANSI code page
        CP_SYMBOL = 42, // SYMBOL translations

        CP_UTF7 = 65000, // UTF-7 translation
        CP_UTF8 = 65001, // UTF-8 translation
    }

    public enum GET_MODULE_HANDLE_EX
    {
        GET_MODULE_HANDLE_EX_FLAG_PIN = 0x00000001,
        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT = 0x00000002,
        GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS = 0x00000004
    }

    public enum LOCALE_USE : uint
    {
        LOCALE_UNSPECIFIED = 0,
        LOCALE_NOUSEROVERRIDE = 0x80000000
    }

    [Flags]
    public enum LCID
    {
        LCID_INSTALLED = 0x00000001, // installed locale ids
        LCID_SUPPORTED = 0x00000002, // supported locale ids
        LCID_ALTERNATE_SORTS = 0x00000004, // alternate sort locale ids
    }

    public enum DATE
    {
        /// DATE_SHORTDATE -> 0x00000001
        DATE_SHORTDATE = 1,

        /// DATE_LONGDATE -> 0x00000002
        DATE_LONGDATE = 2,

        /// DATE_USE_ALT_CALENDAR -> 0x00000004
        DATE_USE_ALT_CALENDAR = 4,

        /// DATE_YEARMONTH -> 0x00000008
        DATE_YEARMONTH = 8,

        /// DATE_LTRREADING -> 0x00000010
        DATE_LTRREADING = 16,

        /// DATE_RTLREADING -> 0x00000020
        DATE_RTLREADING = 32,

        /// DATE_AUTOLAYOUT -> 0x00000040
        DATE_AUTOLAYOUT = 64,
    }

    public enum THREAD
    {
        RUN_IMMEDIATELY = 0,

        /// CREATE_SUSPENDED -> 0x00000004
        CREATE_SUSPENDED = 4,

        /// STACK_SIZE_PARAM_IS_A_RESERVATION -> 0x00010000
        STACK_SIZE_PARAM_IS_A_RESERVATION = 65536,
    }

    public enum PIPE
    {
        /// PIPE_ACCESS_INBOUND -> 0x00000001
        PIPE_ACCESS_INBOUND = 1,

        /// PIPE_ACCESS_OUTBOUND -> 0x00000002
        PIPE_ACCESS_OUTBOUND = 2,

        /// PIPE_ACCESS_DUPLEX -> 0x00000003
        PIPE_ACCESS_DUPLEX = 3,

        /// PIPE_CLIENT_END -> 0x00000000
        PIPE_CLIENT_END = 0,

        /// PIPE_SERVER_END -> 0x00000001
        PIPE_SERVER_END = 1,

        /// PIPE_WAIT -> 0x00000000
        PIPE_WAIT = 0,

        /// PIPE_NOWAIT -> 0x00000001
        PIPE_NOWAIT = 1,

        /// PIPE_READMODE_BYTE -> 0x00000000
        PIPE_READMODE_BYTE = 0,

        /// PIPE_READMODE_MESSAGE -> 0x00000002
        PIPE_READMODE_MESSAGE = 2,

        /// PIPE_TYPE_BYTE -> 0x00000000
        PIPE_TYPE_BYTE = 0,

        /// PIPE_TYPE_MESSAGE -> 0x00000004
        PIPE_TYPE_MESSAGE = 4,

        /// PIPE_UNLIMITED_INSTANCES -> 255
        PIPE_UNLIMITED_INSTANCES = 255,

        /// PIPE_ACCEPT_REMOTE_CLIENTS -> 0x00000000
        PIPE_ACCEPT_REMOTE_CLIENTS = 0,

        /// PIPE_REJECT_REMOTE_CLIENTS -> 0x00000008
        PIPE_REJECT_REMOTE_CLIENTS = 8,
    }

    public enum FIND_FIRST
    {
        FIND_FIRST_EX_CASE_SENSITIVE = 0x00000001,
        FIND_FIRST_EX_LARGE_FETCH = 0x00000002
    }

    public enum MUI_LANGUAGE_ENUM
    {

        /// MUI_LANGUAGE_ID -> 0x4
        MUI_LANGUAGE_ID = 4,

        /// MUI_LANGUAGE_NAME -> 0x8
        MUI_LANGUAGE_NAME = 8
    }

    public enum MUI
    {
        /// MUI_MERGE_SYSTEM_FALLBACK -> 0x10
        MUI_MERGE_SYSTEM_FALLBACK = 16,

        /// MUI_MERGE_USER_FALLBACK -> 0x20
        MUI_MERGE_USER_FALLBACK = 32,

        /// MUI_UI_FALLBACK -> MUI_MERGE_SYSTEM_FALLBACK | MUI_MERGE_USER_FALLBACK
        MUI_UI_FALLBACK = (MUI_MERGE_SYSTEM_FALLBACK | MUI_MERGE_USER_FALLBACK),

        /// MUI_THREAD_LANGUAGES -> 0x40
        MUI_THREAD_LANGUAGES = 64,

        /// MUI_CONSOLE_FILTER -> 0x100
        MUI_CONSOLE_FILTER = 256,

        /// MUI_COMPLEX_SCRIPT_FILTER -> 0x200
        MUI_COMPLEX_SCRIPT_FILTER = 512,

        /// MUI_RESET_FILTERS -> 0x001
        MUI_RESET_FILTERS = 1,

        /// MUI_USER_PREFERRED_UI_LANGUAGES -> 0x10
        MUI_USER_PREFERRED_UI_LANGUAGES = 16,

        /// MUI_USE_INSTALLED_LANGUAGES -> 0x20
        MUI_USE_INSTALLED_LANGUAGES = 32,

        /// MUI_USE_SEARCH_ALL_LANGUAGES -> 0x40
        MUI_USE_SEARCH_ALL_LANGUAGES = 64,

        /// MUI_LANG_NEUTRAL_PE_FILE -> 0x100
        MUI_LANG_NEUTRAL_PE_FILE = 256,

        /// MUI_NON_LANG_NEUTRAL_FILE -> 0x200
        MUI_NON_LANG_NEUTRAL_FILE = 512,

        /// MUI_MACHINE_LANGUAGE_SETTINGS -> 0x400
        MUI_MACHINE_LANGUAGE_SETTINGS = 1024,

        /// MUI_FILETYPE_NOT_LANGUAGE_NEUTRAL -> 0x001
        MUI_FILETYPE_NOT_LANGUAGE_NEUTRAL = 1,

        /// MUI_FILETYPE_LANGUAGE_NEUTRAL_MAIN -> 0x002
        MUI_FILETYPE_LANGUAGE_NEUTRAL_MAIN = 2,

        /// MUI_FILETYPE_LANGUAGE_NEUTRAL_MUI -> 0x004
        MUI_FILETYPE_LANGUAGE_NEUTRAL_MUI = 4,

        /// MUI_QUERY_TYPE -> 0x001
        MUI_QUERY_TYPE = 1,

        /// MUI_QUERY_CHECKSUM -> 0x002
        MUI_QUERY_CHECKSUM = 2,

        /// MUI_QUERY_LANGUAGE_NAME -> 0x004
        MUI_QUERY_LANGUAGE_NAME = 4,

        /// MUI_QUERY_RESOURCE_TYPES -> 0x008
        MUI_QUERY_RESOURCE_TYPES = 8,

        /// MUI_FILEINFO_VERSION -> 0x001
        MUI_FILEINFO_VERSION = 1
    }

    public enum MUI_LANGUAGE
    {
        /// MUI_FULL_LANGUAGE -> 0x01
        MUI_FULL_LANGUAGE = 1,

        /// MUI_PARTIAL_LANGUAGE -> 0x02
        MUI_PARTIAL_LANGUAGE = 2,

        /// MUI_LIP_LANGUAGE -> 0x04
        MUI_LIP_LANGUAGE = 4,

        /// MUI_LANGUAGE_INSTALLED -> 0x20
        MUI_LANGUAGE_INSTALLED = 32,

        /// MUI_LANGUAGE_LICENSED -> 0x40
        MUI_LANGUAGE_LICENSED = 64,
    }

    public enum TIME_FORMAT_ENUM
    {
        USE_CURRENT = 0,
        TIME_NOSECONDS = 2,
        LOCAL_USE_CP_ACP = 0x40000000
    }

    public enum DDD
    {
        DDD_RAW_TARGET_PATH = 0x00000001,
        DDD_REMOVE_DEFINITION = 0x00000002,
        DDD_EXACT_MATCH_ON_REMOVE = 0x00000004,
        DDD_NO_BROADCAST_SYSTEM = 0x00000008,
        DDD_LUID_BROADCAST_DRIVE = 0x00000010
    }

    public enum DEACTIVATE_ACTCTX_FLAG
    {
        DEACTIVATE_ACTCTX_FLAG_TOP_FRAME = 0,
        DEACTIVATE_ACTCTX_FLAG_FORCE_EARLY_DEACTIVATION = 1
    }

    public enum EXCEPTION
    {
        EXCEPTION_CONTINUABLE = 0,
        EXCEPTION_NONCONTINUABLE = 1
    }

    [Flags]
    public enum TIME_FORMAT : uint
    {

        /// TIME_NOMINUTESORSECONDS -> 0x00000001
        TIME_NOMINUTESORSECONDS = 1,

        /// TIME_NOSECONDS -> 0x00000002
        TIME_NOSECONDS = 2,

        /// TIME_NOTIMEMARKER -> 0x00000004
        TIME_NOTIMEMARKER = 4,

        /// TIME_FORCE24HOURFORMAT -> 0x00000008
        TIME_FORCE24HOURFORMAT = 8,
        LOCAL_USE_CP_ACP = 0x40000000,
        LOCALE_NOUSEROVERRIDE = 0x80000000
    }

    public enum LOAD_LIBRARY
    {
        /// DONT_RESOLVE_DLL_REFERENCES -> 0x00000001
        DONT_RESOLVE_DLL_REFERENCES = 1,

        /// LOAD_LIBRARY_AS_DATAFILE -> 0x00000002
        LOAD_LIBRARY_AS_DATAFILE = 2,

        /// LOAD_WITH_ALTERED_SEARCH_PATH -> 0x00000008
        LOAD_WITH_ALTERED_SEARCH_PATH = 8,

        /// LOAD_IGNORE_CODE_AUTHZ_LEVEL -> 0x00000010
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16,

        /// LOAD_LIBRARY_AS_IMAGE_RESOURCE -> 0x00000020
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 32,

        /// LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE -> 0x00000040
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 64,

        /// LOAD_LIBRARY_REQUIRE_SIGNED_TARGET -> 0x00000080
        LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 128,
    }

    [Flags]
    public enum FORMAT_MESSAGE
    {
        /// FORMAT_MESSAGE_ALLOCATE_BUFFER -> 0x00000100
        FORMAT_MESSAGE_ALLOCATE_BUFFER = 256,

        /// FORMAT_MESSAGE_IGNORE_INSERTS -> 0x00000200
        FORMAT_MESSAGE_IGNORE_INSERTS = 512,

        /// FORMAT_MESSAGE_FROM_STRING -> 0x00000400
        FORMAT_MESSAGE_FROM_STRING = 1024,

        /// FORMAT_MESSAGE_FROM_HMODULE -> 0x00000800
        FORMAT_MESSAGE_FROM_HMODULE = 2048,

        /// FORMAT_MESSAGE_FROM_SYSTEM -> 0x00001000
        FORMAT_MESSAGE_FROM_SYSTEM = 4096,

        /// FORMAT_MESSAGE_ARGUMENT_ARRAY -> 0x00002000
        FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192,

        /// FORMAT_MESSAGE_MAX_WIDTH_MASK -> 0x000000FF
        FORMAT_MESSAGE_MAX_WIDTH_MASK = 255,

    }

    [Flags]
    public enum CREATE_PROCESS
    {
        /// DEBUG_PROCESS -> 0x00000001
        DEBUG_PROCESS = 1,

        /// DEBUG_ONLY_THIS_PROCESS -> 0x00000002
        DEBUG_ONLY_THIS_PROCESS = 2,

        /// CREATE_SUSPENDED -> 0x00000004
        CREATE_SUSPENDED = 4,

        /// DETACHED_PROCESS -> 0x00000008
        DETACHED_PROCESS = 8,

        /// CREATE_NEW_CONSOLE -> 0x00000010
        CREATE_NEW_CONSOLE = 16,

        /// NORMAL_PRIORITY_CLASS -> 0x00000020
        NORMAL_PRIORITY_CLASS = 32,

        /// IDLE_PRIORITY_CLASS -> 0x00000040
        IDLE_PRIORITY_CLASS = 64,

        /// HIGH_PRIORITY_CLASS -> 0x00000080
        HIGH_PRIORITY_CLASS = 128,

        /// REALTIME_PRIORITY_CLASS -> 0x00000100
        REALTIME_PRIORITY_CLASS = 256,

        /// CREATE_NEW_PROCESS_GROUP -> 0x00000200
        CREATE_NEW_PROCESS_GROUP = 512,

        /// CREATE_UNICODE_ENVIRONMENT -> 0x00000400
        CREATE_UNICODE_ENVIRONMENT = 1024,

        /// CREATE_SEPARATE_WOW_VDM -> 0x00000800
        CREATE_SEPARATE_WOW_VDM = 2048,

        /// CREATE_SHARED_WOW_VDM -> 0x00001000
        CREATE_SHARED_WOW_VDM = 4096,

        /// CREATE_FORCEDOS -> 0x00002000
        CREATE_FORCEDOS = 8192,

        /// BELOW_NORMAL_PRIORITY_CLASS -> 0x00004000
        BELOW_NORMAL_PRIORITY_CLASS = 16384,

        /// ABOVE_NORMAL_PRIORITY_CLASS -> 0x00008000
        ABOVE_NORMAL_PRIORITY_CLASS = 32768,

        /// CREATE_BREAKAWAY_FROM_JOB -> 0x01000000
        CREATE_BREAKAWAY_FROM_JOB = 16777216,

        /// CREATE_PRESERVE_CODE_AUTHZ_LEVEL -> 0x02000000
        CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 33554432,

        /// CREATE_DEFAULT_ERROR_MODE -> 0x04000000
        CREATE_DEFAULT_ERROR_MODE = 67108864,

        /// CREATE_NO_WINDOW -> 0x08000000
        CREATE_NO_WINDOW = 134217728,

        /// PROFILE_USER -> 0x10000000
        PROFILE_USER = 268435456,

        /// PROFILE_KERNEL -> 0x20000000
        PROFILE_KERNEL = 536870912,

        /// PROFILE_SERVER -> 0x40000000
        PROFILE_SERVER = 1073741824,

        /// CREATE_IGNORE_SYSTEM_DEFAULT -> 0x80000000
        CREATE_IGNORE_SYSTEM_DEFAULT = -2147483648,

        /// INHERIT_PARENT_AFFINITY -> 0x00010000
        INHERIT_PARENT_AFFINITY = 65536,

        /// INHERIT_CALLER_PRIORITY -> 0x00020000
        INHERIT_CALLER_PRIORITY = 131072,

        /// CREATE_PROTECTED_PROCESS -> 0x00040000
        CREATE_PROTECTED_PROCESS = 262144,

        /// EXTENDED_STARTUPINFO_PRESENT -> 0x00080000
        EXTENDED_STARTUPINFO_PRESENT = 524288,

        /// PROCESS_MODE_BACKGROUND_BEGIN -> 0x00100000
        PROCESS_MODE_BACKGROUND_BEGIN = 1048576,

        /// PROCESS_MODE_BACKGROUND_END -> 0x00200000
        PROCESS_MODE_BACKGROUND_END = 2097152,
    }

    public enum COMPARE_STRING
    {
        /// NORM_IGNORECASE -> 0x00000001
        NORM_IGNORECASE = 1,

        /// NORM_IGNORENONSPACE -> 0x00000002
        NORM_IGNORENONSPACE = 2,

        /// NORM_IGNORESYMBOLS -> 0x00000004
        NORM_IGNORESYMBOLS = 4,

        /// NORM_IGNOREKANATYPE -> 0x00010000
        NORM_IGNOREKANATYPE = 65536,

        /// NORM_IGNOREWIDTH -> 0x00020000
        NORM_IGNOREWIDTH = 131072,

        /// LINGUISTIC_IGNORECASE -> 0x00000010
        LINGUISTIC_IGNORECASE = 16,

        /// LINGUISTIC_IGNOREDIACRITIC -> 0x00000020
        LINGUISTIC_IGNOREDIACRITIC = 32,

        /// NORM_LINGUISTIC_CASING -> 0x08000000
        NORM_LINGUISTIC_CASING = 134217728,

        SORT_STRINGSORT = 0x00001000,

        SORT_DIGITSASNUMBERS = 8

    }

    public enum GMEM
    {
        /// GMEM_FIXED -> 0x0000
        GMEM_FIXED = 0,

        /// GMEM_MOVEABLE -> 0x0002
        GMEM_MOVEABLE = 2,

        /// GMEM_NOCOMPACT -> 0x0010
        GMEM_NOCOMPACT = 16,

        /// GMEM_NODISCARD -> 0x0020
        GMEM_NODISCARD = 32,

        /// GMEM_ZEROINIT -> 0x0040
        GMEM_ZEROINIT = 64,

        /// GMEM_MODIFY -> 0x0080
        GMEM_MODIFY = 128,

        /// GMEM_DISCARDABLE -> 0x0100
        GMEM_DISCARDABLE = 256,

        /// GMEM_NOT_BANKED -> 0x1000
        GMEM_NOT_BANKED = 4096,

        /// GMEM_SHARE -> 0x2000
        GMEM_SHARE = 8192,

        /// GMEM_DDESHARE -> 0x2000
        GMEM_DDESHARE = 8192,

        /// GMEM_NOTIFY -> 0x4000
        GMEM_NOTIFY = 16384,

        /// GMEM_LOWER -> GMEM_NOT_BANKED
        GMEM_LOWER = GMEM_NOT_BANKED,

        /// GMEM_VALID_FLAGS -> 0x7F72
        GMEM_VALID_FLAGS = 32626,

        /// GMEM_INVALID_HANDLE -> 0x8000
        GMEM_INVALID_HANDLE = 32768,

    }

    public enum WRITE_WATCH
    {
        WRITE_WATCH_UNSPECIFIED = 0,
        WRITE_WATCH_FLAG_RESET = 1
    }

    public enum FIBER_FLAG
    {
        FIBER_FLAG_UNSPECIFIED = 0,
        FIBER_FLAG_FLOAT_SWITCH = 1
    }

    public enum REPLACEFILE
    {
        /// REPLACEFILE_WRITE_THROUGH -> 0x00000001
        REPLACEFILE_WRITE_THROUGH = 1,

        /// REPLACEFILE_IGNORE_MERGE_ERRORS -> 0x00000002
        REPLACEFILE_IGNORE_MERGE_ERRORS = 2,

        /// REPLACEFILE_IGNORE_ACL_ERRORS -> 0x00000004
        REPLACEFILE_IGNORE_ACL_ERRORS = 4,
    }

    public enum LMEM
    {
        /// LMEM_FIXED -> 0x0000
        LMEM_FIXED = 0,

        /// LMEM_MOVEABLE -> 0x0002
        LMEM_MOVEABLE = 2,

        /// LMEM_NOCOMPACT -> 0x0010
        LMEM_NOCOMPACT = 16,

        /// LMEM_NODISCARD -> 0x0020
        LMEM_NODISCARD = 32,

        /// LMEM_ZEROINIT -> 0x0040
        LMEM_ZEROINIT = 64,

        /// LMEM_MODIFY -> 0x0080
        LMEM_MODIFY = 128,

        /// LMEM_DISCARDABLE -> 0x0F00
        LMEM_DISCARDABLE = 3840,

        /// LMEM_VALID_FLAGS -> 0x0F72
        LMEM_VALID_FLAGS = 3954,

        /// LMEM_INVALID_HANDLE -> 0x8000
        LMEM_INVALID_HANDLE = 32768,

    }

    public enum LCMAP
    {
        /// LCMAP_LOWERCASE -> 0x00000100
        LCMAP_LOWERCASE = 256,

        /// LCMAP_UPPERCASE -> 0x00000200
        LCMAP_UPPERCASE = 512,

        /// LCMAP_SORTKEY -> 0x00000400
        LCMAP_SORTKEY = 1024,

        /// LCMAP_BYTEREV -> 0x00000800
        LCMAP_BYTEREV = 2048,

        /// LCMAP_HIRAGANA -> 0x00100000
        LCMAP_HIRAGANA = 1048576,

        /// LCMAP_KATAKANA -> 0x00200000
        LCMAP_KATAKANA = 2097152,

        /// LCMAP_HALFWIDTH -> 0x00400000
        LCMAP_HALFWIDTH = 4194304,

        /// LCMAP_FULLWIDTH -> 0x00800000
        LCMAP_FULLWIDTH = 8388608,

        /// LCMAP_LINGUISTIC_CASING -> 0x01000000
        LCMAP_LINGUISTIC_CASING = 16777216,

        /// LCMAP_SIMPLIFIED_CHINESE -> 0x02000000
        LCMAP_SIMPLIFIED_CHINESE = 33554432,

        /// LCMAP_TRADITIONAL_CHINESE -> 0x04000000
        LCMAP_TRADITIONAL_CHINESE = 67108864,

        /// LCMAP_TITLECASE -> 0x00000300
        LCMAP_TITLECASE = 768,

    }

    public enum HEAP
    {
        /// HEAP_NO_SERIALIZE -> 0x00000001
        HEAP_NO_SERIALIZE = 1,

        /// HEAP_GROWABLE -> 0x00000002
        HEAP_GROWABLE = 2,

        /// HEAP_GENERATE_EXCEPTIONS -> 0x00000004
        HEAP_GENERATE_EXCEPTIONS = 4,

        /// HEAP_ZERO_MEMORY -> 0x00000008
        HEAP_ZERO_MEMORY = 8,

        /// HEAP_REALLOC_IN_PLACE_ONLY -> 0x00000010
        HEAP_REALLOC_IN_PLACE_ONLY = 16,

        /// HEAP_TAIL_CHECKING_ENABLED -> 0x00000020
        HEAP_TAIL_CHECKING_ENABLED = 32,

        /// HEAP_FREE_CHECKING_ENABLED -> 0x00000040
        HEAP_FREE_CHECKING_ENABLED = 64,

        /// HEAP_DISABLE_COALESCE_ON_FREE -> 0x00000080
        HEAP_DISABLE_COALESCE_ON_FREE = 128,

        /// HEAP_CREATE_ALIGN_16 -> 0x00010000
        HEAP_CREATE_ALIGN_16 = 65536,

        /// HEAP_CREATE_ENABLE_TRACING -> 0x00020000
        HEAP_CREATE_ENABLE_TRACING = 131072,

        /// HEAP_CREATE_ENABLE_EXECUTE -> 0x00040000
        HEAP_CREATE_ENABLE_EXECUTE = 262144,

        /// HEAP_MAXIMUM_TAG -> 0x0FFF
        HEAP_MAXIMUM_TAG = 4095,

        /// HEAP_PSEUDO_TAG_FLAG -> 0x8000
        HEAP_PSEUDO_TAG_FLAG = 32768,

        /// HEAP_TAG_SHIFT -> 18
        HEAP_TAG_SHIFT = 18,

    }

    public enum MAP
    {
        /// MAP_FOLDCZONE -> 0x00000010
        MAP_FOLDCZONE = 16,

        /// MAP_PRECOMPOSED -> 0x00000020
        MAP_PRECOMPOSED = 32,

        /// MAP_COMPOSITE -> 0x00000040
        MAP_COMPOSITE = 64,

        /// MAP_FOLDDIGITS -> 0x00000080
        MAP_FOLDDIGITS = 128,

        /// MAP_EXPAND_LIGATURES -> 0x00002000
        MAP_EXPAND_LIGATURES = 8192,
    }

    [Flags]
    public enum FILE_SHARE
    {
        FILE_SHARE_UNSPECIFIED = 0,
        FILE_SHARE_READ = 1,
        FILE_SHARE_WRITE = 2,
        FILE_SHARE_DELETE = 4
    }

    public enum FILE_CREATE
    {
        /// CREATE_NEW -> 1
        CREATE_NEW = 1,

        /// CREATE_ALWAYS -> 2
        CREATE_ALWAYS = 2,

        /// OPEN_EXISTING -> 3
        OPEN_EXISTING = 3,

        /// OPEN_ALWAYS -> 4
        OPEN_ALWAYS = 4,

        /// TRUNCATE_EXISTING -> 5
        TRUNCATE_EXISTING = 5,
    }

    public enum COPY_FILE
    {
        /// COPY_FILE_FAIL_IF_EXISTS -> 0x00000001
        COPY_FILE_FAIL_IF_EXISTS = 1,

        /// COPY_FILE_RESTARTABLE -> 0x00000002
        COPY_FILE_RESTARTABLE = 2,

        /// COPY_FILE_OPEN_SOURCE_FOR_WRITE -> 0x00000004
        COPY_FILE_OPEN_SOURCE_FOR_WRITE = 4,

        /// COPY_FILE_ALLOW_DECRYPTED_DESTINATION -> 0x00000008
        COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 8,

        /// COPY_FILE_COPY_SYMLINK -> 0x00000800
        COPY_FILE_COPY_SYMLINK = 2048,

        /// COPY_FILE_NO_BUFFERING -> 0x00001000
        COPY_FILE_NO_BUFFERING = 4096,
    }

    public enum LOCKFILE
    {
        LOCKFILE_FAIL_IMMEDIATELY = 0x00000001,
        LOCKFILE_EXCLUSIVE_LOCK = 0x00000002
    }

    public enum PURGE
    {
        PURGE_TXABORT = 0x0001, // Kill the pending/current writes to the comm port.
        PURGE_RXABORT = 0x0002, // Kill the pending/current reads to the comm port.
        PURGE_TXCLEAR = 0x0004, // Kill the transmit queue if there.
        PURGE_RXCLEAR = 0x0008, // Kill the typeahead buffer if there.
    }

    [Flags]
    public enum CHARACTER_ATTRIBUTE
    {
        /// FOREGROUND_BLUE -> 0x0001
        FOREGROUND_BLUE = 1,

        /// FOREGROUND_GREEN -> 0x0002
        FOREGROUND_GREEN = 2,

        /// FOREGROUND_RED -> 0x0004
        FOREGROUND_RED = 4,

        /// FOREGROUND_INTENSITY -> 0x0008
        FOREGROUND_INTENSITY = 8,

        /// BACKGROUND_BLUE -> 0x0010
        BACKGROUND_BLUE = 16,

        /// BACKGROUND_GREEN -> 0x0020
        BACKGROUND_GREEN = 32,

        /// BACKGROUND_RED -> 0x0040
        BACKGROUND_RED = 64,

        /// BACKGROUND_INTENSITY -> 0x0080
        BACKGROUND_INTENSITY = 128,

        /// COMMON_LVB_LEADING_BYTE -> 0x0100
        COMMON_LVB_LEADING_BYTE = 256,

        /// COMMON_LVB_TRAILING_BYTE -> 0x0200
        COMMON_LVB_TRAILING_BYTE = 512,

        /// COMMON_LVB_GRID_HORIZONTAL -> 0x0400
        COMMON_LVB_GRID_HORIZONTAL = 1024,

        /// COMMON_LVB_GRID_LVERTICAL -> 0x0800
        COMMON_LVB_GRID_LVERTICAL = 2048,

        /// COMMON_LVB_GRID_RVERTICAL -> 0x1000
        COMMON_LVB_GRID_RVERTICAL = 4096,

        /// COMMON_LVB_REVERSE_VIDEO -> 0x4000
        COMMON_LVB_REVERSE_VIDEO = 16384,

        /// COMMON_LVB_UNDERSCORE -> 0x8000
        COMMON_LVB_UNDERSCORE = 32768,

        /// COMMON_LVB_SBCSDBCS -> 0x0300
        COMMON_LVB_SBCSDBCS = 768,
    }

    public enum FILE_ATTRIBUTE
    {
        /// FILE_ATTRIBUTE_READONLY -> 0x00000001
        FILE_ATTRIBUTE_READONLY = 1,

        /// FILE_ATTRIBUTE_HIDDEN -> 0x00000002
        FILE_ATTRIBUTE_HIDDEN = 2,

        /// FILE_ATTRIBUTE_SYSTEM -> 0x00000004
        FILE_ATTRIBUTE_SYSTEM = 4,

        /// FILE_ATTRIBUTE_DIRECTORY -> 0x00000010
        FILE_ATTRIBUTE_DIRECTORY = 16,

        /// FILE_ATTRIBUTE_ARCHIVE -> 0x00000020
        FILE_ATTRIBUTE_ARCHIVE = 32,

        /// FILE_ATTRIBUTE_DEVICE -> 0x00000040
        FILE_ATTRIBUTE_DEVICE = 64,

        /// FILE_ATTRIBUTE_NORMAL -> 0x00000080
        FILE_ATTRIBUTE_NORMAL = 128,

        /// FILE_ATTRIBUTE_TEMPORARY -> 0x00000100
        FILE_ATTRIBUTE_TEMPORARY = 256,

        /// FILE_ATTRIBUTE_SPARSE_FILE -> 0x00000200
        FILE_ATTRIBUTE_SPARSE_FILE = 512,

        /// FILE_ATTRIBUTE_REPARSE_POINT -> 0x00000400
        FILE_ATTRIBUTE_REPARSE_POINT = 1024,

        /// FILE_ATTRIBUTE_COMPRESSED -> 0x00000800
        FILE_ATTRIBUTE_COMPRESSED = 2048,

        /// FILE_ATTRIBUTE_OFFLINE -> 0x00001000
        FILE_ATTRIBUTE_OFFLINE = 4096,

        /// FILE_ATTRIBUTE_NOT_CONTENT_INDEXED -> 0x00002000
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 8192,

        /// FILE_ATTRIBUTE_ENCRYPTED -> 0x00004000
        FILE_ATTRIBUTE_ENCRYPTED = 16384,

        /// FILE_ATTRIBUTE_VIRTUAL -> 0x00010000
        FILE_ATTRIBUTE_VIRTUAL = 65536,

    }

    public enum CREATE
    {
        CREATE_NORMAL = 0,
        CREATE_READONLY = 1,
        CREATE_HIDDEN = 2,
        CREATE_SYSTEM = 4
    }

    public enum CONSOLE_SELECTION
    {
        /// CONSOLE_NO_SELECTION -> 0x0000
        CONSOLE_NO_SELECTION = 0,

        /// CONSOLE_SELECTION_IN_PROGRESS -> 0x0001
        CONSOLE_SELECTION_IN_PROGRESS = 1,

        /// CONSOLE_SELECTION_NOT_EMPTY -> 0x0002
        CONSOLE_SELECTION_NOT_EMPTY = 2,

        /// CONSOLE_MOUSE_SELECTION -> 0x0004
        CONSOLE_MOUSE_SELECTION = 4,

        /// CONSOLE_MOUSE_DOWN -> 0x0008
        CONSOLE_MOUSE_DOWN = 8,
    }

    public enum AC_STATUS : byte
    {
        AC_OFFLINE = 0,
        AC_ONLINE = 1,
        AC_UNKNOWN = 255
    }

    [Flags]
    public enum BATTERY_STATUS : byte
    {
        BATTERY_HIGH = 1,
        BATTERY_LOW = 2,
        BATTERY_CRITICAL = 4,
        BATTERY_CHARGING = 8,
        BATTERY_NONE = 128,
        BATTERY_UNKNOWN = 255
    }

    [Flags]
    public enum STARTF
    {
        /// STARTF_USESHOWWINDOW -> 0x00000001
        STARTF_USESHOWWINDOW = 1,

        /// STARTF_USESIZE -> 0x00000002
        STARTF_USESIZE = 2,

        /// STARTF_USEPOSITION -> 0x00000004
        STARTF_USEPOSITION = 4,

        /// STARTF_USECOUNTCHARS -> 0x00000008
        STARTF_USECOUNTCHARS = 8,

        /// STARTF_USEFILLATTRIBUTE -> 0x00000010
        STARTF_USEFILLATTRIBUTE = 16,

        /// STARTF_RUNFULLSCREEN -> 0x00000020
        STARTF_RUNFULLSCREEN = 32,

        /// STARTF_FORCEONFEEDBACK -> 0x00000040
        STARTF_FORCEONFEEDBACK = 64,

        /// STARTF_FORCEOFFFEEDBACK -> 0x00000080
        STARTF_FORCEOFFFEEDBACK = 128,

        /// STARTF_USESTDHANDLES -> 0x00000100
        STARTF_USESTDHANDLES = 256,

        /// STARTF_USEHOTKEY -> 0x00000200
        STARTF_USEHOTKEY = 512,

        /// STARTF_TITLEISLINKNAME -> 0x00000800
        STARTF_TITLEISLINKNAME = 2048,

        /// STARTF_TITLEISAPPID -> 0x00001000
        STARTF_TITLEISAPPID = 4096,

        /// STARTF_PREVENTPINNING -> 0x00002000
        STARTF_PREVENTPINNING = 8192,

    }

    public enum PROCESS_HEAP : ushort
    {
        /// PROCESS_HEAP_REGION -> 0x0001
        PROCESS_HEAP_REGION = 1,

        /// PROCESS_HEAP_UNCOMMITTED_RANGE -> 0x0002
        PROCESS_HEAP_UNCOMMITTED_RANGE = 2,

        /// PROCESS_HEAP_ENTRY_BUSY -> 0x0004
        PROCESS_HEAP_ENTRY_BUSY = 4,

        /// PROCESS_HEAP_ENTRY_MOVEABLE -> 0x0010
        PROCESS_HEAP_ENTRY_MOVEABLE = 16,

        /// PROCESS_HEAP_ENTRY_DDESHARE -> 0x0020
        PROCESS_HEAP_ENTRY_DDESHARE = 32,
    }

    public enum ACTCTX_FLAG
    {
        /// ACTCTX_FLAG_PROCESSOR_ARCHITECTURE_VALID -> (0x00000001)
        ACTCTX_FLAG_PROCESSOR_ARCHITECTURE_VALID = 1,

        /// ACTCTX_FLAG_LANGID_VALID -> (0x00000002)
        ACTCTX_FLAG_LANGID_VALID = 2,

        /// ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID -> (0x00000004)
        ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4,

        /// ACTCTX_FLAG_RESOURCE_NAME_VALID -> (0x00000008)
        ACTCTX_FLAG_RESOURCE_NAME_VALID = 8,

        /// ACTCTX_FLAG_SET_PROCESS_DEFAULT -> (0x00000010)
        ACTCTX_FLAG_SET_PROCESS_DEFAULT = 16,

        /// ACTCTX_FLAG_APPLICATION_NAME_VALID -> (0x00000020)
        ACTCTX_FLAG_APPLICATION_NAME_VALID = 32,

        /// ACTCTX_FLAG_SOURCE_IS_ASSEMBLYREF -> (0x00000040)
        ACTCTX_FLAG_SOURCE_IS_ASSEMBLYREF = 64,

        /// ACTCTX_FLAG_HMODULE_VALID -> (0x00000080)
        ACTCTX_FLAG_HMODULE_VALID = 128,

    }

    public enum CONTROL_KEY_STATE
    {
        /// RIGHT_ALT_PRESSED -> 0x0001
        RIGHT_ALT_PRESSED = 1,

        /// LEFT_ALT_PRESSED -> 0x0002
        LEFT_ALT_PRESSED = 2,

        /// RIGHT_CTRL_PRESSED -> 0x0004
        RIGHT_CTRL_PRESSED = 4,

        /// LEFT_CTRL_PRESSED -> 0x0008
        LEFT_CTRL_PRESSED = 8,

        /// SHIFT_PRESSED -> 0x0010
        SHIFT_PRESSED = 16,

        /// NUMLOCK_ON -> 0x0020
        NUMLOCK_ON = 32,

        /// SCROLLLOCK_ON -> 0x0040
        SCROLLLOCK_ON = 64,

        /// CAPSLOCK_ON -> 0x0080
        CAPSLOCK_ON = 128,

        /// ENHANCED_KEY -> 0x0100
        ENHANCED_KEY = 256,

    }

    public enum BUTTON_STATE
    {
        /// FROM_LEFT_1ST_BUTTON_PRESSED -> 0x0001
        FROM_LEFT_1ST_BUTTON_PRESSED = 1,

        /// RIGHTMOST_BUTTON_PRESSED -> 0x0002
        RIGHTMOST_BUTTON_PRESSED = 2,

        /// FROM_LEFT_2ND_BUTTON_PRESSED -> 0x0004
        FROM_LEFT_2ND_BUTTON_PRESSED = 4,

        /// FROM_LEFT_3RD_BUTTON_PRESSED -> 0x0008
        FROM_LEFT_3RD_BUTTON_PRESSED = 8,

        /// FROM_LEFT_4TH_BUTTON_PRESSED -> 0x0010
        FROM_LEFT_4TH_BUTTON_PRESSED = 16,
    }

    public enum MOUSE_EVENT_FLAG
    {

        NONE = 0,
        MOUSE_MOVED = 1,

        /// DOUBLE_CLICK -> 0x0002
        DOUBLE_CLICK = 2,

        /// MOUSE_WHEELED -> 0x0004
        MOUSE_WHEELED = 4,

        /// MOUSE_HWHEELED -> 0x0008
        MOUSE_HWHEELED = 8,
    }

    [Flags]
    public enum CONSOLE_MODE : uint
    {
        ENABLE_ECHO_INPUT = 4U,
        ENABLE_INSERT_MODE = 32U,
        ENABLE_LINE_INPUT = 2U,
        ENABLE_MOUSE_INPUT = 16U,
        ENABLE_PROCESSED_INPUT = 1U,
        ENABLE_PROCESSED_OUTPUT = ENABLE_PROCESSED_INPUT,
        ENABLE_QUICK_EDIT_MODE = 64U,
        ENABLE_WINDOW_INPUT = 8U,
        ENABLE_WRAP_AT_EOL_OUTPUT = ENABLE_LINE_INPUT,
        NONE = 0U,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EXCEPTION_POINTERS
    {
        /// PEXCEPTION_RECORD->EXCEPTION_RECORD*
        public IntPtr ExceptionRecord;

        /// PCONTEXT->CONTEXT*
        public IntPtr ContextRecord;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        /// SHORT->short
        public short X;

        /// SHORT->short
        public short Y;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct LARGE_INTEGER
    {
        /// Anonymous_9320654f_2227_43bf_a385_74cc8c562686
        [FieldOffset(0)]
        public LOW_HIGH lowHigh;

        /// Anonymous_947eb392_1446_4e25_bbd4_10e98165f3a9
        [FieldOffset(0)]
        public LOW_HIGH u;

        /// LONGLONG->__int64
        [FieldOffset(0)]
        public long QuadPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        /// SHORT->short
        public short Left;

        /// SHORT->short
        public short Top;

        /// SHORT->short
        public short Right;

        /// SHORT->short
        public short Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CHAR_INFO
    {
        /// Anonymous_f3630dcb_df39_4f30_a593_48e610e9363d
        public ENCODING_INFO Char;

        /// WORD->unsigned short
        public UInt16 Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        /// DWORD->unsigned int
        public uint nLength;

        /// LPVOID->void*
        public IntPtr lpSecurityDescriptor;

        /// BOOL->int
        [MarshalAs(UnmanagedType.Bool)]
        public bool bInheritHandle;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct GUID
    {
        /// unsigned int
        public uint Data1;

        /// unsigned short
        public ushort Data2;

        /// unsigned short
        public ushort Data3;

        /// unsigned char[8]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Data4;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TIME_ZONE_INFORMATION
    {
        /// LONG->int
        public int Bias;

        /// WCHAR[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string StandardName;

        /// SYSTEMTIME->_SYSTEMTIME
        public SYSTEMTIME StandardDate;

        /// LONG->int
        public int StandardBias;

        /// WCHAR[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DaylightName;

        /// SYSTEMTIME->_SYSTEMTIME
        public SYSTEMTIME DaylightDate;

        /// LONG->int
        public int DaylightBias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        /// WORD->unsigned short
        public ushort wYear;

        /// WORD->unsigned short
        public ushort wMonth;

        /// WORD->unsigned short
        public ushort wDayOfWeek;

        /// WORD->unsigned short
        public ushort wDay;

        /// WORD->unsigned short
        public ushort wHour;

        /// WORD->unsigned short
        public ushort wMinute;

        /// WORD->unsigned short
        public ushort wSecond;

        /// WORD->unsigned short
        public ushort wMilliseconds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_CURSOR_INFO
    {
        /// DWORD->unsigned int
        public uint dwSize;

        /// BOOL->int
        [MarshalAs(UnmanagedType.Bool)]
        public bool bVisible;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT_RECORD
    {
        /// WORD->unsigned short
        public ushort EventType;

        /// Anonymous_79fe9041_6876_475e_b93a_ffb0d7822836
        public EVENT_INFO Event;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CURRENCYFMTW
    {
        /// UINT->unsigned int
        public uint NumDigits;

        /// UINT->unsigned int
        public uint LeadingZero;

        /// UINT->unsigned int
        public uint Grouping;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpDecimalSep;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpThousandSep;

        /// UINT->unsigned int
        public uint NegativeOrder;

        /// UINT->unsigned int
        public uint PositiveOrder;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpCurrencySymbol;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CURRENCYFMTA
    {
        /// UINT->unsigned int
        public uint NumDigits;

        /// UINT->unsigned int
        public uint LeadingZero;

        /// UINT->unsigned int
        public uint Grouping;

        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpDecimalSep;

        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpThousandSep;

        /// UINT->unsigned int
        public uint NegativeOrder;

        /// UINT->unsigned int
        public uint PositiveOrder;

        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpCurrencySymbol;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONTEXT
    {
        /// DWORD->unsigned int
        public uint ContextFlags;

        /// DWORD->unsigned int
        public uint Dr0;

        /// DWORD->unsigned int
        public uint Dr1;

        /// DWORD->unsigned int
        public uint Dr2;

        /// DWORD->unsigned int
        public uint Dr3;

        /// DWORD->unsigned int
        public uint Dr6;

        /// DWORD->unsigned int
        public uint Dr7;

        /// FLOATING_SAVE_AREA->_FLOATING_SAVE_AREA
        public FLOATING_SAVE_AREA FloatSave;

        /// DWORD->unsigned int
        public uint SegGs;

        /// DWORD->unsigned int
        public uint SegFs;

        /// DWORD->unsigned int
        public uint SegEs;

        /// DWORD->unsigned int
        public uint SegDs;

        /// DWORD->unsigned int
        public uint Edi;

        /// DWORD->unsigned int
        public uint Esi;

        /// DWORD->unsigned int
        public uint Ebx;

        /// DWORD->unsigned int
        public uint Edx;

        /// DWORD->unsigned int
        public uint Ecx;

        /// DWORD->unsigned int
        public uint Eax;

        /// DWORD->unsigned int
        public uint Ebp;

        /// DWORD->unsigned int
        public uint Eip;

        /// DWORD->unsigned int
        public uint SegCs;

        /// DWORD->unsigned int
        public uint EFlags;

        /// DWORD->unsigned int
        public uint Esp;

        /// DWORD->unsigned int
        public uint SegSs;

        /// BYTE[512]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
        public byte[] ExtendedRegisters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NUMBERFMTW
    {
        /// UINT->unsigned int
        public uint NumDigits;

        /// UINT->unsigned int
        public uint LeadingZero;

        /// UINT->unsigned int
        public uint Grouping;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpDecimalSep;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpThousandSep;

        /// UINT->unsigned int
        public uint NegativeOrder;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NUMBERFMTA
    {
        /// UINT->unsigned int
        public uint NumDigits;

        /// UINT->unsigned int
        public uint LeadingZero;

        /// UINT->unsigned int
        public uint Grouping;

        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpDecimalSep;

        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpThousandSep;

        /// UINT->unsigned int
        public uint NegativeOrder;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FILE_SEGMENT_ELEMENT
    {
        /// PVOID64->void*
        [FieldOffset(0)]
        public IntPtr Buffer;

        /// ULONGLONG->unsigned __int64
        [FieldOffset(0)]
        public ulong Alignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SINGLE_LIST_ENTRY
    {
        /// _SINGLE_LIST_ENTRY*
        public IntPtr Next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        /// ULONG_PTR->unsigned int
        public uint Internal;

        /// ULONG_PTR->unsigned int
        public uint InternalHigh;

        /// Anonymous_7416d31a_1ce9_4e50_b1e1_0f2ad25c0196
        public DATA_INFO DataInfo;

        /// HANDLE->void*
        public IntPtr hEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BY_HANDLE_FILE_INFORMATION
    {
        /// DWORD->unsigned int
        public FILE_ATTRIBUTE dwFileAttributes;

        /// FILETIME->_FILETIME
        public FILETIME ftCreationTime;

        /// FILETIME->_FILETIME
        public FILETIME ftLastAccessTime;

        /// FILETIME->_FILETIME
        public FILETIME ftLastWriteTime;

        /// DWORD->unsigned int
        public uint dwVolumeSerialNumber;

        /// DWORD->unsigned int
        public uint nFileSizeHigh;

        /// DWORD->unsigned int
        public uint nFileSizeLow;

        /// DWORD->unsigned int
        public uint nNumberOfLinks;

        /// DWORD->unsigned int
        public uint nFileIndexHigh;

        /// DWORD->unsigned int
        public uint nFileIndexLow;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SCREEN_BUFFER_INFO
    {
        /// COORD->_COORD
        public COORD dwSize;

        /// COORD->_COORD
        public COORD dwCursorPosition;

        /// WORD->unsigned short
        public CHARACTER_ATTRIBUTE wAttributes;

        /// SMALL_RECT->_SMALL_RECT
        public SMALL_RECT srWindow;

        /// COORD->_COORD
        public COORD dwMaximumWindowSize;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SLIST_HEADER
    {
        /// ULONGLONG->unsigned __int64
        [FieldOffset(0)]
        public ulong Alignment;

        /// Anonymous_fd626461_7f3e_49a1_aabe_a2b90f0df936
        [FieldOffset(0)]
        public LIST_ITEM_INFO ListItemInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ACTCTX_SECTION_KEYED_DATA
    {
        /// ULONG->unsigned int
        public uint cbSize;

        /// ULONG->unsigned int
        public uint ulDataFormatVersion;

        /// PVOID->void*
        public IntPtr lpData;

        /// ULONG->unsigned int
        public uint ulLength;

        /// PVOID->void*
        public IntPtr lpSectionGlobalData;

        /// ULONG->unsigned int
        public uint ulSectionGlobalDataLength;

        /// PVOID->void*
        public IntPtr lpSectionBase;

        /// ULONG->unsigned int
        public uint ulSectionTotalLength;

        /// HANDLE->void*
        public IntPtr hActCtx;

        /// ULONG->unsigned int
        public uint ulAssemblyRosterIndex;

        /// ULONG->unsigned int
        public uint ulFlags;

        /// ACTCTX_SECTION_KEYED_DATA_ASSEMBLY_METADATA->tagACTCTX_SECTION_KEYED_DATA_ASSEMBLY_METADATA
        public ACTCTX_SECTION_KEYED_DATA_ASSEMBLY_METADATA AssemblyMetadata;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DCB
    {
        /// DWORD->unsigned int
        public uint DCBlength;

        /// DWORD->unsigned int
        public uint BaudRate;

        /// fBinary : 1
        ///fParity : 1
        ///fOutxCtsFlow : 1
        ///fOutxDsrFlow : 1
        ///fDtrControl : 2
        ///fDsrSensitivity : 1
        ///fTXContinueOnXoff : 1
        ///fOutX : 1
        ///fInX : 1
        ///fErrorChar : 1
        ///fNull : 1
        ///fRtsControl : 2
        ///fAbortOnError : 1
        ///fDummy2 : 17
        public uint bitvector1;

        /// WORD->unsigned short
        public ushort wReserved;

        /// WORD->unsigned short
        public ushort XonLim;

        /// WORD->unsigned short
        public ushort XoffLim;

        /// BYTE->unsigned char
        public byte ByteSize;

        /// BYTE->unsigned char
        public byte Parity;

        /// BYTE->unsigned char
        public byte StopBits;

        /// char
        public byte XonChar;

        /// char
        public byte XoffChar;

        /// char
        public byte ErrorChar;

        /// char
        public byte EofChar;

        /// char
        public byte EvtChar;

        /// WORD->unsigned short
        public ushort wReserved1;

        public uint fBinary
        {
            get
            {
                return (((bitvector1 & 1u)));
            }
            set
            {
                bitvector1 = (((value | bitvector1)));
            }
        }

        public uint fParity
        {
            get
            {
                return ((((bitvector1 & 2u)
                          / 2)));
            }
            set
            {
                bitvector1 = ((((value * 2)
                                | bitvector1)));
            }
        }

        public uint fOutxCtsFlow
        {
            get
            {
                return ((((bitvector1 & 4u)
                          / 4)));
            }
            set
            {
                bitvector1 = ((((value * 4)
                                | bitvector1)));
            }
        }

        public uint fOutxDsrFlow
        {
            get
            {
                return ((((bitvector1 & 8u)
                          / 8)));
            }
            set
            {
                bitvector1 = ((((value * 8)
                                | bitvector1)));
            }
        }

        public uint fDtrControl
        {
            get
            {
                return ((((bitvector1 & 48u)
                          / 16)));
            }
            set
            {
                bitvector1 = ((((value * 16)
                                | bitvector1)));
            }
        }

        public uint fDsrSensitivity
        {
            get
            {
                return ((((bitvector1 & 64u)
                          / 64)));
            }
            set
            {
                bitvector1 = ((((value * 64)
                                | bitvector1)));
            }
        }

        public uint fTXContinueOnXoff
        {
            get
            {
                return ((((bitvector1 & 128u)
                          / 128)));
            }
            set
            {
                bitvector1 = ((((value * 128)
                                | bitvector1)));
            }
        }

        public uint fOutX
        {
            get
            {
                return ((((bitvector1 & 256u)
                          / 256)));
            }
            set
            {
                bitvector1 = ((((value * 256)
                                | bitvector1)));
            }
        }

        public uint fInX
        {
            get
            {
                return ((((bitvector1 & 512u)
                          / 512)));
            }
            set
            {
                bitvector1 = ((((value * 512)
                                | bitvector1)));
            }
        }

        public uint fErrorChar
        {
            get
            {
                return ((((bitvector1 & 1024u)
                          / 1024)));
            }
            set
            {
                bitvector1 = ((((value * 1024)
                                | bitvector1)));
            }
        }

        public uint fNull
        {
            get
            {
                return ((((bitvector1 & 2048u)
                          / 2048)));
            }
            set
            {
                bitvector1 = ((((value * 2048)
                                | bitvector1)));
            }
        }

        public uint fRtsControl
        {
            get
            {
                return ((((bitvector1 & 12288u)
                          / 4096)));
            }
            set
            {
                bitvector1 = ((((value * 4096)
                                | bitvector1)));
            }
        }

        public uint fAbortOnError
        {
            get
            {
                return ((((bitvector1 & 16384u)
                          / 16384)));
            }
            set
            {
                bitvector1 = ((((value * 16384)
                                | bitvector1)));
            }
        }

        public uint fDummy2
        {
            get
            {
                return ((((bitvector1 & 4294934528u)
                          / 32768)));
            }
            set
            {
                bitvector1 = ((((value * 32768)
                                | bitvector1)));
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMMTIMEOUTS
    {
        /// DWORD->unsigned int
        public uint ReadIntervalTimeout;

        /// DWORD->unsigned int
        public uint ReadTotalTimeoutMultiplier;

        /// DWORD->unsigned int
        public uint ReadTotalTimeoutConstant;

        /// DWORD->unsigned int
        public uint WriteTotalTimeoutMultiplier;

        /// DWORD->unsigned int
        public uint WriteTotalTimeoutConstant;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SELECTION_INFO
    {
        /// DWORD->unsigned int
        public CONSOLE_SELECTION dwFlags;

        /// COORD->_COORD
        public COORD dwSelectionAnchor;

        /// SMALL_RECT->_SMALL_RECT
        public SMALL_RECT srSelection;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COMMCONFIG
    {
        /// DWORD->unsigned int
        public uint dwSize;

        /// WORD->unsigned short
        public ushort wVersion;

        /// WORD->unsigned short
        public ushort wReserved;

        /// DCB->_DCB
        public DCB dcb;

        /// DWORD->unsigned int
        public uint dwProviderSubType;

        /// DWORD->unsigned int
        public uint dwProviderOffset;

        /// DWORD->unsigned int
        public uint dwProviderSize;

        /// WCHAR[1]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string wcProviderData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_FONT_INFO
    {
        /// DWORD->unsigned int
        public uint nFont;

        /// COORD->_COORD
        public COORD dwFontSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORYSTATUSEX
    {
        /// DWORD->unsigned int
        public uint dwLength;

        /// DWORD->unsigned int
        public uint dwMemoryLoad;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullTotalPhys;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullAvailPhys;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullTotalPageFile;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullAvailPageFile;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullTotalVirtual;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullAvailVirtual;

        /// DWORDLONG->ULONGLONG->unsigned __int64
        public ulong ullAvailExtendedVirtual;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_POWER_STATUS
    {
        /// BYTE->unsigned char
        public AC_STATUS ACLineStatus;

        /// BYTE->unsigned char
        public BATTERY_STATUS BatteryFlag;

        /// BYTE->unsigned char
        public byte BatteryLifePercent;

        /// BYTE->unsigned char
        public byte Reserved1;

        /// DWORD->unsigned int
        public uint BatteryLifeTime;

        /// DWORD->unsigned int
        public uint BatteryFullLifeTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_INFO
    {
        /// Anonymous_459bdf75_2992_4fef_9fb5_389952f59d12
        public PROCESSOR_INFO processorInfo;

        /// DWORD->unsigned int
        public uint dwPageSize;

        /// LPVOID->void*
        public IntPtr lpMinimumApplicationAddress;

        /// LPVOID->void*
        public IntPtr lpMaximumApplicationAddress;

        /// DWORD_PTR->ULONG_PTR->unsigned int
        public uint dwActiveProcessorMask;

        /// DWORD->unsigned int
        public uint dwNumberOfProcessors;

        /// DWORD->unsigned int
        public uint dwProcessorType;

        /// DWORD->unsigned int
        public uint dwAllocationGranularity;

        /// WORD->unsigned short
        public ushort wProcessorLevel;

        /// WORD->unsigned short
        public ushort wProcessorRevision;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct RTL_OSVERSIONINFOEX
    {
        /// DWORD->unsigned int
        public uint dwOSVersionInfoSize;

        /// DWORD->unsigned int
        public uint dwMajorVersion;

        /// DWORD->unsigned int
        public uint dwMinorVersion;

        /// DWORD->unsigned int
        public uint dwBuildNumber;

        /// DWORD->unsigned int
        public uint dwPlatformId;

        /// WCHAR[128]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;

        /// WORD->unsigned short
        public ushort wServicePackMajor;

        /// WORD->unsigned short
        public ushort wServicePackMinor;

        /// WORD->unsigned short
        public ushort wSuiteMask;

        /// BYTE->unsigned char
        public byte wProductType;

        /// BYTE->unsigned char
        public byte wReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORYSTATUS
    {
        /// DWORD->unsigned int
        public uint dwLength;

        /// DWORD->unsigned int
        public uint dwMemoryLoad;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwTotalPhys;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwAvailPhys;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwTotalPageFile;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwAvailPageFile;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwTotalVirtual;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint dwAvailVirtual;
    }

    //[StructLayout(LayoutKind.Sequential)]
    //public struct DEBUG_EVENT
    //{
    //    /// DWORD->unsigned int
    //    public uint dwDebugEventCode;

    //    /// DWORD->unsigned int
    //    public uint dwProcessId;

    //    /// DWORD->unsigned int
    //    public uint dwThreadId;

    //    /// Anonymous_c0f5f2ce_b74f_423c_81ee_f2fb7ef45eb7
    //    public EXCEPTION_INFO u;
    //}

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COMMPROP
    {
        /// WORD->unsigned short
        public ushort wPacketLength;

        /// WORD->unsigned short
        public ushort wPacketVersion;

        /// DWORD->unsigned int
        public uint dwServiceMask;

        /// DWORD->unsigned int
        public uint dwReserved1;

        /// DWORD->unsigned int
        public uint dwMaxTxQueue;

        /// DWORD->unsigned int
        public uint dwMaxRxQueue;

        /// DWORD->unsigned int
        public uint dwMaxBaud;

        /// DWORD->unsigned int
        public uint dwProvSubType;

        /// DWORD->unsigned int
        public uint dwProvCapabilities;

        /// DWORD->unsigned int
        public uint dwSettableParams;

        /// DWORD->unsigned int
        public uint dwSettableBaud;

        /// WORD->unsigned short
        public ushort wSettableData;

        /// WORD->unsigned short
        public ushort wSettableStopParity;

        /// DWORD->unsigned int
        public uint dwCurrentTxQueue;

        /// DWORD->unsigned int
        public uint dwCurrentRxQueue;

        /// DWORD->unsigned int
        public uint dwProvSpec1;

        /// DWORD->unsigned int
        public uint dwProvSpec2;

        /// WCHAR[1]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string wcProvChar;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO
    {
        /// DWORD->unsigned int
        public uint cb;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpReserved;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDesktop;

        /// LPWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpTitle;

        /// DWORD->unsigned int
        public uint dwX;

        /// DWORD->unsigned int
        public uint dwY;

        /// DWORD->unsigned int
        public uint dwXSize;

        /// DWORD->unsigned int
        public uint dwYSize;

        /// DWORD->unsigned int
        public uint dwXCountChars;

        /// DWORD->unsigned int
        public uint dwYCountChars;

        /// DWORD->unsigned int
        public CHARACTER_ATTRIBUTE dwFillAttribute;

        /// DWORD->unsigned int
        public STARTF dwFlags;

        /// WORD->unsigned short
        public ushort wShowWindow;

        /// WORD->unsigned short
        public ushort cbReserved2;

        /// LPBYTE->BYTE*
        public IntPtr lpReserved2;

        /// HANDLE->void*
        public IntPtr hStdInput;

        /// HANDLE->void*
        public IntPtr hStdOutput;

        /// HANDLE->void*
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_BASIC_INFORMATION
    {
        /// PVOID->void*
        public IntPtr BaseAddress;

        /// PVOID->void*
        public IntPtr AllocationBase;

        /// DWORD->unsigned int
        public uint AllocationProtect;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint RegionSize;

        /// DWORD->unsigned int
        public uint State;

        /// DWORD->unsigned int
        public uint Protect;

        /// DWORD->unsigned int
        public uint Type;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIN32_FIND_DATA
    {
        /// DWORD->unsigned int
        public FILE_ATTRIBUTE dwFileAttributes;

        /// FILETIME->_FILETIME
        public FILETIME ftCreationTime;

        /// FILETIME->_FILETIME
        public FILETIME ftLastAccessTime;

        /// FILETIME->_FILETIME
        public FILETIME ftLastWriteTime;

        /// DWORD->unsigned int
        public uint nFileSizeHigh;

        /// DWORD->unsigned int
        public uint nFileSizeLow;

        /// DWORD->unsigned int
        public uint dwReserved0;

        /// DWORD->unsigned int
        public uint dwReserved1;

        /// WCHAR[260]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;

        /// WCHAR[14]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        /// HANDLE->void*
        public IntPtr hProcess;

        /// HANDLE->void*
        public IntPtr hThread;

        /// DWORD->unsigned int
        public uint dwProcessId;

        /// DWORD->unsigned int
        public uint dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMSTAT
    {
        /// fCtsHold : 1
        ///fDsrHold : 1
        ///fRlsdHold : 1
        ///fXoffHold : 1
        ///fXoffSent : 1
        ///fEof : 1
        ///fTxim : 1
        ///fReserved : 25
        public uint bitvector1;

        /// DWORD->unsigned int
        public uint cbInQue;

        /// DWORD->unsigned int
        public uint cbOutQue;

        public uint fCtsHold
        {
            get
            {
                return (((bitvector1 & 1u)));
            }
            set
            {
                bitvector1 = (((value | bitvector1)));
            }
        }

        public uint fDsrHold
        {
            get
            {
                return ((((bitvector1 & 2u)
                          / 2)));
            }
            set
            {
                bitvector1 = ((((value * 2)
                                | bitvector1)));
            }
        }

        public uint fRlsdHold
        {
            get
            {
                return ((((bitvector1 & 4u)
                          / 4)));
            }
            set
            {
                bitvector1 = ((((value * 4)
                                | bitvector1)));
            }
        }

        public uint fXoffHold
        {
            get
            {
                return ((((bitvector1 & 8u)
                          / 8)));
            }
            set
            {
                bitvector1 = ((((value * 8)
                                | bitvector1)));
            }
        }

        public uint fXoffSent
        {
            get
            {
                return ((((bitvector1 & 16u)
                          / 16)));
            }
            set
            {
                bitvector1 = ((((value * 16)
                                | bitvector1)));
            }
        }

        public uint fEof
        {
            get
            {
                return ((((bitvector1 & 32u)
                          / 32)));
            }
            set
            {
                bitvector1 = ((((value * 32)
                                | bitvector1)));
            }
        }

        public uint fTxim
        {
            get
            {
                return ((((bitvector1 & 64u)
                          / 64)));
            }
            set
            {
                bitvector1 = ((((value * 64)
                                | bitvector1)));
            }
        }

        public uint fReserved
        {
            get
            {
                return ((((bitvector1 & 4294967168u)
                          / 128)));
            }
            set
            {
                bitvector1 = ((((value * 128)
                                | bitvector1)));
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct RTL_OSVERSIONINFO
    {
        /// DWORD->unsigned int
        public uint dwOSVersionInfoSize;

        /// DWORD->unsigned int
        public uint dwMajorVersion;

        /// DWORD->unsigned int
        public uint dwMinorVersion;

        /// DWORD->unsigned int
        public uint dwBuildNumber;

        /// DWORD->unsigned int
        public uint dwPlatformId;

        /// WCHAR[128]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CPINFOEX
    {
        /// UINT->unsigned int
        public uint MaxCharSize;

        /// BYTE[2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] DefaultChar;

        /// BYTE[12]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
        public byte[] LeadByte;

        /// WCHAR->wchar_t->unsigned short
        public ushort UnicodeDefaultChar;

        /// UINT->unsigned int
        public uint CodePage;

        /// WCHAR[260]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string CodePageName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JOB_SET_ARRAY
    {
        /// HANDLE->void*
        public IntPtr JobHandle;

        /// DWORD->unsigned int
        public uint MemberLevel;

        /// DWORD->unsigned int
        public uint Flags;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct OFSTRUCT
    {
        /// BYTE->unsigned char
        public byte cBytes;

        /// BYTE->unsigned char
        public byte fFixedDisk;

        /// WORD->unsigned short
        public ushort nErrCode;

        /// WORD->unsigned short
        public ushort Reserved1;

        /// WORD->unsigned short
        public ushort Reserved2;

        /// CHAR[128]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPathName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CPINFO
    {
        /// UINT->unsigned int
        public uint MaxCharSize;

        /// BYTE[2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] DefaultChar;

        /// BYTE[12]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
        public byte[] LeadByte;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PROCESS_HEAP_ENTRY
    {
        /// PVOID->void*
        public IntPtr lpData;

        /// DWORD->unsigned int
        public uint cbData;

        /// BYTE->unsigned char
        public byte cbOverhead;

        /// BYTE->unsigned char
        public byte iRegionIndex;

        /// WORD->unsigned short
        public PROCESS_HEAP wFlags;

        /// Anonymous_53c79322_6506_4260_871f_234e2af0cacb
        public BLOCK_REGION BlockRegion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IO_COUNTERS
    {
        /// ULONGLONG->unsigned __int64
        public ulong ReadOperationCount;

        /// ULONGLONG->unsigned __int64
        public ulong WriteOperationCount;

        /// ULONGLONG->unsigned __int64
        public ulong OtherOperationCount;

        /// ULONGLONG->unsigned __int64
        public ulong ReadTransferCount;

        /// ULONGLONG->unsigned __int64
        public ulong WriteTransferCount;

        /// ULONGLONG->unsigned __int64
        public ulong OtherTransferCount;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ULARGE_INTEGER
    {
        /// Anonymous_652f900e_e9d5_4a81_ba95_5c3af2ba5157
        [FieldOffset(0)]
        public LOW_HIGH lowHigh;

        /// Anonymous_da3d5bb2_d7f6_4b49_a86f_df044e26e59a
        [FieldOffset(0)]
        public LOW_HIGH u;

        /// ULONGLONG->unsigned __int64
        [FieldOffset(0)]
        public ulong QuadPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ACTCTX
    {
        /// ULONG->unsigned int
        public uint cbSize;

        /// DWORD->unsigned int
        public ACTCTX_FLAG dwFlags;

        /// LPCWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpSource;

        /// USHORT->unsigned short
        public ushort wProcessorArchitecture;

        /// LANGID->WORD->unsigned short
        public ushort wLangId;

        /// LPCWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpAssemblyDirectory;

        /// LPCWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpResourceName;

        /// LPCWSTR->WCHAR*
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpApplicationName;

        /// HMODULE->HINSTANCE->HINSTANCE__*
        public IntPtr hModule;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ENCODING_INFO
    {
        /// WCHAR->wchar_t->unsigned short
        [FieldOffset(0)]
        public ushort UnicodeChar;

        /// CHAR->char
        [FieldOffset(0)]
        public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct EVENT_INFO
    {
        /// KEY_EVENT_RECORD->_KEY_EVENT_RECORD
        [FieldOffset(0)]
        public KEY_EVENT_RECORD KeyEvent;

        /// MOUSE_EVENT_RECORD->_MOUSE_EVENT_RECORD
        [FieldOffset(0)]
        public MOUSE_EVENT_RECORD MouseEvent;

        /// WINDOW_BUFFER_SIZE_RECORD->_WINDOW_BUFFER_SIZE_RECORD
        [FieldOffset(0)]
        public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;

        /// MENU_EVENT_RECORD->_MENU_EVENT_RECORD
        [FieldOffset(0)]
        public MENU_EVENT_RECORD MenuEvent;

        /// FOCUS_EVENT_RECORD->_FOCUS_EVENT_RECORD
        [FieldOffset(0)]
        public FOCUS_EVENT_RECORD FocusEvent;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DATA_INFO
    {
        /// Anonymous_ac6e4301_4438_458f_96dd_e86faeeca2a6
        [FieldOffset(0)]
        public OFFSET_INFO offsetInfo;

        /// PVOID->void*
        [FieldOffset(0)]
        public IntPtr Pointer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LIST_ITEM_INFO
    {
        /// SINGLE_LIST_ENTRY->_SINGLE_LIST_ENTRY
        public SINGLE_LIST_ENTRY Next;

        /// WORD->unsigned short
        public ushort Depth;

        /// WORD->unsigned short
        public ushort Sequence;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct PROCESSOR_INFO
    {
        /// DWORD->unsigned int
        [FieldOffset(0)]
        public uint dwOemId;

        /// Anonymous_a30d5f78_3b46_471a_9d25_915a0fe3987d
        [FieldOffset(0)]
        public PROCESSOR_ARCHITECTURE processorArchitecture;
    }

    //[StructLayout(LayoutKind.Explicit)]
    //public struct EXCEPTION_INFO
    //{
    //    /// EXCEPTION_DEBUG_INFO->_EXCEPTION_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public EXCEPTION_DEBUG_INFO Exception;

    //    /// CREATE_THREAD_DEBUG_INFO->_CREATE_THREAD_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public CREATE_THREAD_DEBUG_INFO CreateThread;

    //    /// CREATE_PROCESS_DEBUG_INFO->_CREATE_PROCESS_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public CREATE_PROCESS_DEBUG_INFO CreateProcessInfo;

    //    /// EXIT_THREAD_DEBUG_INFO->_EXIT_THREAD_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public EXIT_THREAD_DEBUG_INFO ExitThread;

    //    /// EXIT_PROCESS_DEBUG_INFO->_EXIT_PROCESS_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public EXIT_PROCESS_DEBUG_INFO ExitProcess;

    //    /// LOAD_DLL_DEBUG_INFO->_LOAD_DLL_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public LOAD_DLL_DEBUG_INFO LoadDll;

    //    /// UNLOAD_DLL_DEBUG_INFO->_UNLOAD_DLL_DEBUG_INFO
    //    [FieldOffset(0)]
    //    public UNLOAD_DLL_DEBUG_INFO UnloadDll;

    //    /// OUTPUT_DEBUG_STRING_INFO->_OUTPUT_DEBUG_STRING_INFO
    //    [FieldOffset(0)]
    //    public OUTPUT_DEBUG_STRING_INFO DebugString;

    //    /// RIP_INFO->_RIP_INFO
    //    [FieldOffset(0)]
    //    public RIP_INFO RipInfo;
    //}

    [StructLayout(LayoutKind.Explicit)]
    internal struct BLOCK_REGION
    {
        /// Anonymous_8fdd9a27_0167_4a68_9d58_0eb653f5b1e6
        [FieldOffset(0)]
        public BLOCK_DATA Block;

        /// Anonymous_e469aa29_5a46_4c74_b009_60adb4b669c8
        [FieldOffset(0)]
        public REGION_DATA Region;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RTL_CRITICAL_SECTION
    {
        /// PRTL_CRITICAL_SECTION_DEBUG->_RTL_CRITICAL_SECTION_DEBUG*
        public IntPtr DebugInfo;

        /// LONG->int
        public int LockCount;

        /// LONG->int
        public int RecursionCount;

        /// HANDLE->void*
        public IntPtr OwningThread;

        /// HANDLE->void*
        public IntPtr LockSemaphore;

        /// ULONG_PTR->unsigned int
        public uint SpinCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LDT_ENTRY
    {
        /// WORD->unsigned short
        public ushort LimitLow;

        /// WORD->unsigned short
        public ushort BaseLow;

        /// Anonymous_6ec22b34_e8ab_46f2_a900_b8a44fc9e55d
        public HIGH_WORD HighWord;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLOATING_SAVE_AREA
    {
        /// DWORD->unsigned int
        public uint ControlWord;

        /// DWORD->unsigned int
        public uint StatusWord;

        /// DWORD->unsigned int
        public uint TagWord;

        /// DWORD->unsigned int
        public uint ErrorOffset;

        /// DWORD->unsigned int
        public uint ErrorSelector;

        /// DWORD->unsigned int
        public uint DataOffset;

        /// DWORD->unsigned int
        public uint DataSelector;

        /// BYTE[80]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
        public byte[] RegisterArea;

        /// DWORD->unsigned int
        public uint Cr0NpxState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ACTCTX_SECTION_KEYED_DATA_ASSEMBLY_METADATA
    {
        /// PVOID->void*
        public IntPtr lpInformation;

        /// PVOID->void*
        public IntPtr lpSectionBase;

        /// ULONG->unsigned int
        public uint ulSectionLength;

        /// PVOID->void*
        public IntPtr lpSectionGlobalDataBase;

        /// ULONG->unsigned int
        public uint ulSectionGlobalDataLength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EXCEPTION_RECORD
    {
        /// DWORD->unsigned int
        public uint ExceptionCode;

        /// DWORD->unsigned int
        public EXCEPTION ExceptionFlags;

        /// _EXCEPTION_RECORD*
        public IntPtr ExceptionRecord;

        /// PVOID->void*
        public IntPtr ExceptionAddress;

        /// DWORD->unsigned int
        public uint NumberParameters;

        /// ULONG_PTR[15]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.U4)]
        public uint[] ExceptionInformation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LOW_HIGH
    {
        /// DWORD->unsigned int
        public uint LowPart;

        /// DWORD->unsigned int
        public uint HighPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OFFSET_INFO
    {
        /// DWORD->unsigned int
        public uint Offset;

        /// DWORD->unsigned int
        public uint OffsetHigh;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESSOR_ARCHITECTURE
    {
        /// WORD->unsigned short
        public ushort wProcessorArchitecture;

        /// WORD->unsigned short
        public ushort wReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BLOCK_DATA
    {
        /// HANDLE->void*
        public IntPtr hMem;

        /// DWORD[3]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] dwReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct REGION_DATA
    {
        /// DWORD->unsigned int
        public uint dwCommittedSize;

        /// DWORD->unsigned int
        public uint dwUnCommittedSize;

        /// LPVOID->void*
        public IntPtr lpFirstBlock;

        /// LPVOID->void*
        public IntPtr lpLastBlock;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HIGH_WORD
    {
        /// Anonymous_715d4807_68f7_43b3_b3b6_d2fd0500eaa4
        [FieldOffset(0)]
        public BYTES_INFO Bytes;

        /// Anonymous_75f63f95_3c9e_4dd3_9ea4_4111163afbd5
        [FieldOffset(0)]
        public BITS_INFO Bits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEY_EVENT_RECORD
    {
        /// BOOL->int
        [MarshalAs(UnmanagedType.Bool)]
        public bool bKeyDown;

        /// WORD->unsigned short
        public ushort wRepeatCount;

        /// WORD->unsigned short
        public ushort wVirtualKeyCode;

        /// WORD->unsigned short
        public ushort wVirtualScanCode;

        /// Anonymous_ee4ad878_dde2_4d9b_b7de_b1588db350c7
        public ENCODING_INFO uChar;

        /// DWORD->unsigned int
        public uint dwControlKeyState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSE_EVENT_RECORD
    {
        /// COORD->_COORD
        public COORD dwMousePosition;

        /// DWORD->unsigned int
        public BUTTON_STATE dwButtonState;

        /// DWORD->unsigned int
        public CONTROL_KEY_STATE dwControlKeyState;

        /// DWORD->unsigned int
		public MOUSE_EVENT_FLAG dwEventFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOW_BUFFER_SIZE_RECORD
    {
        /// COORD->_COORD
        public COORD dwSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MENU_EVENT_RECORD
    {
        /// UINT->unsigned int
        public uint dwCommandId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FOCUS_EVENT_RECORD
    {
        /// BOOL->int
        [MarshalAs(UnmanagedType.Bool)]
        public bool bSetFocus;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EXCEPTION_DEBUG_INFO
    {
        /// EXCEPTION_RECORD->_EXCEPTION_RECORD
        public EXCEPTION_RECORD ExceptionRecord;

        /// DWORD->unsigned int
        public uint dwFirstChance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CREATE_THREAD_DEBUG_INFO
    {
        /// HANDLE->void*
        public IntPtr hThread;

        /// LPVOID->void*
        public IntPtr lpThreadLocalBase;

        /// LPTHREAD_START_ROUTINE->PTHREAD_START_ROUTINE
        public Kernel32.PTHREAD_START_ROUTINE lpStartAddress;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CREATE_PROCESS_DEBUG_INFO
    {
        /// HANDLE->void*
        public IntPtr hFile;

        /// HANDLE->void*
        public IntPtr hProcess;

        /// HANDLE->void*
        public IntPtr hThread;

        /// LPVOID->void*
        public IntPtr lpBaseOfImage;

        /// DWORD->unsigned int
        public uint dwDebugInfoFileOffset;

        /// DWORD->unsigned int
        public uint nDebugInfoSize;

        /// LPVOID->void*
        public IntPtr lpThreadLocalBase;

        /// LPTHREAD_START_ROUTINE->PTHREAD_START_ROUTINE
        public Kernel32.PTHREAD_START_ROUTINE lpStartAddress;

        /// LPVOID->void*
        public IntPtr lpImageName;

        /// WORD->unsigned short
        public ushort fUnicode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EXIT_THREAD_DEBUG_INFO
    {
        /// DWORD->unsigned int
        public uint dwExitCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EXIT_PROCESS_DEBUG_INFO
    {
        /// DWORD->unsigned int
        public uint dwExitCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LOAD_DLL_DEBUG_INFO
    {
        /// HANDLE->void*
        public IntPtr hFile;

        /// LPVOID->void*
        public IntPtr lpBaseOfDll;

        /// DWORD->unsigned int
        public uint dwDebugInfoFileOffset;

        /// DWORD->unsigned int
        public uint nDebugInfoSize;

        /// LPVOID->void*
        public IntPtr lpImageName;

        /// WORD->unsigned short
        public ushort fUnicode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct UNLOAD_DLL_DEBUG_INFO
    {
        /// LPVOID->void*
        public IntPtr lpBaseOfDll;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OUTPUT_DEBUG_STRING_INFO
    {
        /// LPSTR->CHAR*
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpDebugStringData;

        /// WORD->unsigned short
        public ushort fUnicode;

        /// WORD->unsigned short
        public ushort nDebugStringLength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RIP_INFO
    {
        /// DWORD->unsigned int
        public uint dwError;

        /// DWORD->unsigned int
        public uint dwType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BYTES_INFO
    {
        /// BYTE->unsigned char
        public byte BaseMid;

        /// BYTE->unsigned char
        public byte Flags1;

        /// BYTE->unsigned char
        public byte Flags2;

        /// BYTE->unsigned char
        public byte BaseHi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITS_INFO
    {
        /// BaseMid : 8
        ///Type : 5
        ///Dpl : 2
        ///Pres : 1
        ///LimitHi : 4
        ///Sys : 1
        ///Reserved_0 : 1
        ///Default_Big : 1
        ///Granularity : 1
        ///BaseHi : 8
        public uint bitvector1;

        public uint BaseMid
        {
            get
            {
                return (((bitvector1 & 255u)));
            }

            set
            {
                bitvector1 = (((value | bitvector1)));
            }
        }

        public uint Type
        {
            get
            {
                return ((((bitvector1 & 7936u)
                          / 256)));
            }

            set
            {
                bitvector1 = ((((value * 256)
                                | bitvector1)));
            }
        }

        public uint Dpl
        {
            get
            {
                return ((((bitvector1 & 24576u)
                          / 8192)));
            }

            set
            {
                bitvector1 = ((((value * 8192)
                                | bitvector1)));
            }
        }

        public uint Pres
        {
            get
            {
                return ((((bitvector1 & 32768u)
                          / 32768)));
            }

            set
            {
                bitvector1 = ((((value * 32768)
                                | bitvector1)));
            }
        }

        public uint LimitHi
        {
            get
            {
                return ((((bitvector1 & 983040u)
                          / 65536)));
            }

            set
            {
                bitvector1 = ((((value * 65536)
                                | bitvector1)));
            }
        }

        public uint Sys
        {
            get
            {
                return ((((bitvector1 & 1048576u)
                          / 1048576)));
            }

            set
            {
                bitvector1 = ((((value * 1048576)
                                | bitvector1)));
            }
        }

        public uint Reserved_0
        {
            get
            {
                return ((((bitvector1 & 2097152u)
                          / 2097152)));
            }

            set
            {
                bitvector1 = ((((value * 2097152)
                                | bitvector1)));
            }
        }

        public uint Default_Big
        {
            get
            {
                return ((((bitvector1 & 4194304u)
                          / 4194304)));
            }

            set
            {
                bitvector1 = ((((value * 4194304)
                                | bitvector1)));
            }
        }

        public uint Granularity
        {
            get
            {
                return ((((bitvector1 & 8388608u)
                          / 8388608)));
            }

            set
            {
                bitvector1 = ((((value * 8388608)
                                | bitvector1)));
            }
        }

        public uint BaseHi
        {
            get
            {
                return ((((bitvector1 & 4278190080u)
                          / 16777216)));
            }

            set
            {
                bitvector1 = ((((value * 16777216)
                                | bitvector1)));
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RTL_RESOURCE_DEBUG
    {
        /// WORD->unsigned short
        public ushort Type;

        /// WORD->unsigned short
        public ushort CreatorBackTraceIndex;

        /// _RTL_CRITICAL_SECTION*
        public IntPtr CriticalSection;

        /// LIST_ENTRY->_LIST_ENTRY
        public LIST_ENTRY ProcessLocksList;

        /// DWORD->unsigned int
        public uint EntryCount;

        /// DWORD->unsigned int
        public uint ContentionCount;

        /// DWORD[2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        public uint[] Spare;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LIST_ENTRY
    {
        /// _LIST_ENTRY*
        public IntPtr Flink;

        /// _LIST_ENTRY*
        public IntPtr Blink;
    }

	public struct CONSOLE_SCREEN_BUFFER_INFOEX
	{

		/// ULONG->unsigned int
		public uint cbSize;

		/// COORD->_COORD
		public COORD dwSize;

		/// COORD->_COORD
		public COORD dwCursorPosition;

		/// WORD->unsigned short
		public ushort wAttributes;

		/// SMALL_RECT->_SMALL_RECT
		public SMALL_RECT srWindow;

		/// COORD->_COORD
		public COORD dwMaximumWindowSize;

		/// WORD->unsigned short
		public ushort wPopupAttributes;

		/// BOOL->int
		[MarshalAsAttribute(UnmanagedType.Bool)]
		public bool bFullscreenSupported;

		/// COLORREF[16]
		[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.U4)]
		public uint[] ColorTable;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public class CONSOLE_FONT_INFOEX
	{
		private int cbSize;
		public CONSOLE_FONT_INFOEX()
		{
			cbSize = Marshal.SizeOf(typeof(CONSOLE_FONT_INFOEX));
		}
		public int FontIndex;
		public short FontWidth;
		public short FontHeight;
		public int FontFamily;
		public int FontWeight;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string FaceName;
	}

}
