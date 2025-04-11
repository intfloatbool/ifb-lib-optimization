#include "ZeuxMeshOptimizerInterface.h"

// caller should dispose tuple.val2 (uint array) by 'delete[]' !
std::tuple<size_t, unsigned int*> ZeuxMeshOptimizerInterface::SimplifyMeshFast(float* vertexPositions, size_t vertCount, unsigned int* indices, size_t indicesCount, float targetRatio, float errorThreshold)
{
    size_t vertexCount = vertCount / 3;

    assert(vertexCount != 0);
    assert(indicesCount != 0);
    
    auto outIndices = new unsigned int[indicesCount];

    size_t newIndicesSize = meshopt_simplify(
        outIndices,                  // output
        indices,                     // input indices
        indicesCount,                         // number of indices
        vertexPositions,             // vertex positions
        vertexCount,                        // number of vertices
        sizeof(float) * 3,                  // vertex stride
        indicesCount * targetRatio,                        // target index count / original count
        errorThreshold                      // allowable error
    );

    return std::make_tuple(newIndicesSize, outIndices);
}
