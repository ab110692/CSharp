package br.com.avancertecnologia.model.drink;

public class Bonus {
    private String nome;
    private int porcentagem;
    private int custo;

    public Bonus() {
        this.nome = "Bônus de dinheiro";
        this.porcentagem = 0;
        this.custo = 2;
    }

    //region get set
    public String getNome() {
        return nome;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }

    public int getPorcentagem() {
        return porcentagem;
    }

    public void setPorcentagem(int porcentagem) {
        this.porcentagem = porcentagem;
    }

    public int getCusto() {
        return custo;
    }

    public void setCusto(int custo) {
        this.custo = custo;
    }
    //endregion

    public void evoluir(){
        this.setPorcentagem(this.getPorcentagem()+2);
        this.setCusto(this.getCusto()*2);
    }

}
