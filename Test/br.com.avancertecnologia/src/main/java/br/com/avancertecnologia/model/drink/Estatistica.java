package br.com.avancertecnologia.model.drink;

import java.io.Serializable;

public class Estatistica implements Serializable {
    private long quantidadeClick;
    private double valor;
    private int quantidadeDiamante;

    //region Get Set
    public long getQuantidadeClick() {
        return quantidadeClick;
    }

    private void setQuantidadeClick(long quantidadeClick) {
        this.quantidadeClick = quantidadeClick;
    }

    public double getValor() {
        return valor;
    }

    private void setValor(double valor) {
        this.valor = valor;
    }

    public int getQuantidadeDiamante() {
        return quantidadeDiamante;
    }

    private void setQuantidadeDiamante(int quantidadeDiamante) {
        this.quantidadeDiamante = quantidadeDiamante;
    }
    //endregion

    public void addClick(){
        setQuantidadeClick(getQuantidadeClick()+1);
    }

    public void addValor(double valor){
        setValor(getValor() + valor);
    }

    public void addDiamante(int quantidade){
        setQuantidadeDiamante(getQuantidadeDiamante() + quantidade);
    }

}
