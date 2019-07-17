package br.com.avancertecnologia.util.task.listeners;

public interface TaskListener<T> {
    void inicio();
    void atualizar(T t);
    void atualizar(T t, int porcetagem);
    void termino();
}
