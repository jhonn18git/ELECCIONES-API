<?php

namespace App\Http\Controllers;

use App\Models\Cliente;
use Illuminate\Http\Request;

class ClienteController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $clientes = Cliente::all();
        return response()->json($clientes, 200);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
       if ($request->attributes->get('user')->role !== 'admin') {
            return response()->json(['message' => 'Acceso denegado'], 403);
        }
        $cliente = Cliente::create($request->all());
        return response()->json($cliente, 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Cliente $cliente)
    {
        return response()->json($cliente, 200);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, Cliente $cliente)
    {
         if ($request->attributes->get('user')->role !== 'admin') {
            return response()->json(['message' => 'Acceso denegado'], 403);
        }
        $cliente->update($request->all());
        return response()->json($cliente, 200);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Request $request, Cliente $cliente)
    {
        if ($request->attributes->get('user')->role !== 'admin') {
            return response()->json(['message' => 'Acceso denegado'], 403);
        }
        $cliente->delete();
        return response()->json($cliente, 204);
    }
}
