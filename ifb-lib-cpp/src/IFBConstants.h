#ifndef IFB_CONSTANTS_H_
#define IFB_CONSTANTS_H_

#include <string>
#include <stdexcept>
#include <iostream>
#include <thread>

namespace IFBLib
{
	inline void LOG(const std::string msg)
	{
#if IFB_LIB_DEBUG
		std::cout << "IFBLIB LOG -> " << msg <<  std::endl;
#endif
	}

	inline void LOG_ERROR(const std::string msg)
	{
#if IFB_LIB_DEBUG
		std::cout << "IFBLIB ERROR -> " << msg << std::endl;
#endif
	}

	inline float Clamp(float v, float min, float max)
	{
		if (v > max)
		{
			return max;
		}

		if (v < min)
		{
			return min;
		}

		return v;
	}
}

#endif