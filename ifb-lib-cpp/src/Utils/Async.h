#pragma once
#include <future>
#include <type_traits>
namespace IFBLib::Utils
{
	// uses explicit std::launch::async arg for std::async
	template<typename F, typename... Ts>
	inline auto DirectAsync(F&& f, Ts&&... params)
	{
		return std::async(std::launch::async, std::forward<F>(f), std::forward<Ts>(params)...);
	}
}