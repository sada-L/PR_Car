using PR_Car;
AvtoStore avtoStore = new AvtoStore();
while (true) {
    Avto schAvto = avtoStore.AccMenu();
    schAvto.Menu(avtoStore.Acc);
}
