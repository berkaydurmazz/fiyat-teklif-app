import { Button, Container, FormControl, Grid, LinearProgress, TextField } from '@mui/material';
import React from 'react';
import { MuiTelInput, matchIsValidTel } from 'mui-tel-input';
import { useState } from 'react';
import validator from 'validator';
import axios from 'axios';
import { toast } from 'react-toastify';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';

const sleep = (delay) => new Promise((resolve) => setTimeout(resolve, delay));

function UrunEkle() {
    const { id } = useParams();

    const [fetchingError, setFetchingError] = useState(false);
    const [isFetching, setIsFetching] = useState(false);
    const [isUpdate, setIsUpdate] = useState(0);
    const [productName, setProductName] = React.useState('');
    //const [phoneError, setPhoneError] = React.useState(false);
    const [productDescp, setProductDescp] = useState('');
    const [productSize, setProductSize] = useState('');
    const [price, setPrice] = useState('');
    const [suppliers, setSuppliers] = useState('');
    const [kdvRate, setKdvRate] = useState('');
    const [category, setCategory] = useState('');
    const [validationErrors, setValidationErrors] = React.useState({});

    useEffect(() => {
        console.log(id);
        if (typeof id !== 'undefined') {
            setIsUpdate(id);
            setIsFetching(true);
            UrunGetirPromise();
        } else {
            setproductName('');
            setproductDescp('');
            setproductSize('');
            setPrice('');
            setSuppliers('');
            setKdvRate('');
            setCategory('');
            setIsFetching(false);
        }
    }, [id]);

    const handleNumber = (value, info) => {
        setPhone(info.numberValue);
        if (matchIsValidTel(value) || info.nationalNumber === '') {
            setPhoneError(false);
        } else {
            setPhoneError(true);
        }
    };

    const handleEmail = (email) => {
        setEmail(email.target.value);
        if (validator.isEmail(email.target.value) || email.target.value === '') {
            setEmailError(false);
        } else {
            setEmailError(true);
        }
    };

    const UrunEkle = () => {
        if (typeof id !== 'undefined') {
            toast.promise(UrunEklePromise, {
                pending: 'ürün güncelleniyor',
                success: productName + ' başarıyla güncellendi 👌',
                error: productName + ' güncellenirken hata oluştu 🤯'
            });
        } else {
            toast.promise(musteriEklePromise, {
                pending: 'Ürün kaydı yapılıyor',
                success: productName + ' başarıyla eklendi 👌',
                error: productName + ' eklenirken hata oluştu 🤯'
            });
        }
    };

    const UrunEklePromise = () => {
        return new Promise(async (resolve, reject) => {
            const start = Date.now();
            setValidationErrors({});
            let data = JSON.stringify({
                id: typeof id !== 'undefined' ? id : 0,
                adi: musteriAdi,
                soyadi: musteriSoyadi,
                telefonNumarasi: phone,
                email: email
            });

            let config = {
                method: 'post',
                maxBodyLength: Infinity,
                url: 'http://localhost:5273/api/Urun/CreateOrUpdate',
                headers: {
                    'Content-Type': 'application/json',
                    Accept: 'text/plain'
                },
                data: data
            };

            axios
                .request(config)
                .then(async (response) => {
                    console.log(JSON.stringify(response.data));
                    if (response.data.result) {
                        const millis = Date.now() - start;
                        if (millis < 700) {
                            await sleep(700 - millis);
                        }
                        resolve(response.data); // Başarılı sonuç durumunda Promise'ı çöz
                    } else {
                        reject(new Error('İşlem başarısız')); // Başarısız sonuç durumunda Promise'ı reddet
                    }
                })
                .catch((error) => {
                    console.log(error);
                    setValidationErrors(error.response.data.errors);
                    reject(error); // Hata durumunda Promise'ı reddet
                });
        });
    };

    const UrunGetirPromise = () => {
        return new Promise(async (resolve, reject) => {
            const start = Date.now();
            setValidationErrors({});
            let config = {
                method: 'post',
                maxBodyLength: Infinity,
                url: 'http://localhost:5273/api/Urun/Get',
                headers: {
                    'Content-Type': 'application/json',
                    Accept: 'text/plain'
                },
                params: {
                    id: id
                }
            };

            axios
                .request(config)
                .then(async (response) => {
                    console.log(JSON.stringify(response.data));
                    if (response.data.result) {
                        const millis = Date.now() - start;
                        if (millis < 500) {
                            await sleep(500 - millis);
                        }
                        console.log(response.data);
                        setProductName(response.data.data.productName);
                        setProductDescp(response.data.data.productDescp);
                        setProductSize(response.data.data.productSize);
                        setPrice(response.data.data.price);
                        setSuppliers(response.data.data.suppliers);
                        setKdvRate(response.data.data.kdvRate);
                        setCategory(response.data.data.category);
                        setFetchingError(false);
                        resolve(response.data); // Başarılı sonuç d1urumunda Promise'ı çöz
                    } else {
                        setFetchingError(true);
                        reject(new Error('İşlem başarısız')); // Başarısız sonuç durumunda Promise'ı reddet
                    }
                })
                .catch((error) => {
                    setFetchingError(true);
                    console.log(error);
                    reject(error); // Hata durumunda Promise'ı reddet
                })
                .finally(() => {
                    setIsFetching(false);
                });
        });
    };

    return (
        <>
            <Container className="d-flex justify-content-center" maxWidth="md">
                <Grid item xs={6}>
                    <FormControl sx={{ m: 0, width: '50ch' }}>
                        {isFetching && <LinearProgress className="mt-3" color="secondary" />}
                        {(isUpdate === 0 || !isFetching) && (
                            <>
                                <TextField
                                    value={productName}
                                    margin="normal"
                                    id="productName"
                                    label="Ürün Adı"
                                    variant="outlined"
                                    onChange={(e) => setProductName(e.target.value)}
                                    error={!!validationErrors.productName} // Hatanın varlığına göre error özelliğini ayarla
                                    helperText={validationErrors.productName} // Hata mesajını helperText olarak göster
                                />
                                <TextField
                                    margin="normal"
                                    value={productDescp}
                                    id="productDescp"
                                    label="Ürün Açıklaması"
                                    variant="outlined"
                                    onChange={(e) => setProductDescp(e.target.value)}
                                    error={!!validationErrors.productDescp}
                                    helperText={validationErrors.productDescp}
                                />
                                <TextField
                                    margin="normal"
                                    id="productSize"
                                    value={productSize}
                                    label="Ebadı"
                                    variant="outlined"
                                    onChange={(e) => setProductSize(e.target.value)}
                                />
                                <TextField
                                    // error={emailError || !!validationErrors.Email}
                                    // helperText={emailError ? 'Email adresini kontrol edin' : validationErrors.Email} // emailError true ise kendi mesajını göster, aksi halde validationErrors'tan gelen mesajı göster
                                    type="number"
                                    margin="normal"
                                    id="price"
                                    label="Fiyatı"
                                    variant="outlined"
                                    value={price}
                                    onChange={(e) => setPrice(e.target.value)}
                                    //onChange={(e) => handleEmail(e)}
                                />
                                <TextField
                                    margin="normal"
                                    id="suppliers"
                                    value={suppliers}
                                    label="Tedarikçi Firma"
                                    variant="outlined"
                                    onChange={(e) => setSuppliers(e.target.value)}
                                />
                                <TextField
                                    type="number"
                                    margin="normal"
                                    id="kdvRate"
                                    value={kdvRate}
                                    label="KDV Oranı"
                                    variant="outlined"
                                    onChange={(e) => setKdvRate(e.target.value)}
                                />
                                <TextField
                                    margin="normal"
                                    id="category"
                                    value={category}
                                    label="Kategori"
                                    variant="outlined"
                                    onChange={(e) => setCategory(e.target.value)}
                                />
                                <MuiTelInput
                                //    error={phoneerror || !!validationerrors.telefonnumarasi}
                                //    helpertext={phoneerror ? 'telefon numarasını kontrol edin' : validationerrors.telefonnumarasi}
                                //    defaultcountry="tr"
                                //    preferredcountries={['tr']}
                                //    variant="outlined"
                                //    margin="normal"
                                //    label="telefon numarası"
                                //    value={phone}
                                //    onchange={(value, info) => handlenumber(value, info)}
                                //    id="phone-number"
                                //    focusonselectcountry
                                //    forcecallingcode
                                />
                                <Button onClick={urunEkle} className="mb-2" margin="normal" variant="contained">
                                    Kaydet
                                </Button>
                            </>
                        )}
                    </FormControl>
                </Grid>
            </Container>
        </>
    );
}

export default UrunEkle;
