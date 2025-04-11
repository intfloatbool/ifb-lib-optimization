cmake_minimum_required (VERSION 3.8)

if (EXISTS "${CMAKE_BINARY_DIR}/CMakeCache.txt")
    file(REMOVE "${CMAKE_BINARY_DIR}/CMakeCache.txt")
endif()

if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

if(UNIX AND NOT APPLE)
	# for android support when build in linux *.so (linux shared library)
    set(CMAKE_POSITION_INDEPENDENT_CODE ON)
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fPIC")
endif()

project ("ifblib")

# Add source to this project's executable.
file(GLOB_RECURSE SOURCES "src/*.cpp")
#
# -DBUILD_AS_LIBRARY=ON -DBUILD_SHARED=ON

option(BUILD_AS_LIBRARY "Build as library" OFF)
option(BUILD_SHARED "Build as dynamic library" OFF)
option(BUILD_DEBUG "Build as Debug" OFF)

message(STATUS "BUILD_AS_LIBRARY = ${BUILD_AS_LIBRARY}")
message(STATUS "BUILD_SHARED = ${BUILD_SHARED}")
message(STATUS "BUILD_DEBUG = ${BUILD_DEBUG}")

##########
if(BUILD_AS_LIBRARY)
	if(BUILD_SHARED)
		set(LIB_TYPE SHARED)
	else()
		set(LIB_TYPE STATIC)
	endif()
	add_library(ifblib ${LIB_TYPE} ${SOURCES})
else()
	add_executable (ifblib ${SOURCES})
endif()

if(BUILD_DEBUG)
	target_compile_definitions(ifblib PUBLIC IFB_LIB_DEBUG)
endif()

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET ifblib PROPERTY CXX_STANDARD 20)
endif()
