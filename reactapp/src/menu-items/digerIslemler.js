// assets
import { IconUsers } from '@tabler/icons';

// constant
const icons = { IconUsers };

// ==============================|| DASHBOARD MENU ITEMS ||============================== //

const digerIslemler = {
    id: 'digerIslemler',
    title: 'Diğer İşlemler',
    type: 'group',
    children: [
        {
            id: 'musteriler',
            title: 'Müşteriler',
            type: 'collapse',
            icon: icons.IconUsers,

            children: [
                {
                    id: 'musteriler',
                    title: 'Müşteri Listesi',
                    type: 'item',
                    url: '/digerIslemler/musteriler'
                },
                {
                    id: 'musteri-ekle',
                    title: 'Müşteri Ekle',
                    type: 'item',
                    url: '/digerIslemler/musteri-ekle'
                }
            ]
        },
        {
            id: 'urunler',
            title: 'Ürünler',
            type: 'collapse',
            icon: icons.IconUsers,

            children: [
                {
                    id: 'urunler',
                    title: 'Ürün Listesi',
                    type: 'item',
                    url: '/digerIslemler/urunler'
                },
                {
                    id: 'urun-ekle',
                    title: 'Ürün Ekle',
                    type: 'item',
                    url: '/digerIslemler/urun-ekle'
                }
            ]
        },
        {
            id: 'categories',
            title: 'Kategoriler',
            type: 'collapse',
            icon: icons.IconUsers,

            children: [
                {
                    id: 'categories',
                    title: 'Kategoriler',
                    type: 'item',
                    url: '/digerIslemler/kategoriler'
                },
                {
                    id: 'kategori-ekle',
                    title: 'Kategori Ekle',
                    type: 'item',
                    url: '/digerIslemler/kategori-ekle'
                }
            ]
        }
    ]
};

export default digerIslemler;
