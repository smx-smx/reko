cmake_minimum_required(VERSION 3.10 FATAL_ERROR)
list(INSERT CMAKE_MODULE_PATH 0 "${REKO_SRC}/../cmake")

include(msbuild2cmake)

if(WIN32 AND NOT REKO_COMPILER)
    set(REKO_COMPILER "Visual Studio 17 2022")
endif()

set(cmake_arguments "")
list(APPEND cmake_arguments
	-DCMAKE_BUILD_TYPE=${CMAKE_BUILD_TYPE}
	-DREKO_SRC=${REKO_SRC}
	-DREKO_PLATFORM=${REKO_PLATFORM}
	-DTARGET=${TARGET}
)

if("${TARGET}" STREQUAL "git_hash")
	list(APPEND cmake_arguments
		-DGIT_HASH_OUTPUT=${GIT_HASH_OUTPUT}
	)
endif()


invoke_cmake(
	BUILD_DIR ${CMAKE_BINARY_DIR}/build/${TARGET}
	DIRECTORY ${REKO_SRC}/../
	TARGET ${TARGET}
	GENERATOR ${REKO_COMPILER}
	# variables needed by CMakeLists.txt, that must be forwarded
	EXTRA_ARGUMENTS ${cmake_arguments}	
)